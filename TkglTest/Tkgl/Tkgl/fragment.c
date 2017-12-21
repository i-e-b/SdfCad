#version 440 core


// This shader is adapted from https://www.shadertoy.com/view/Xds3zN by Inigo Quilez

in vec4 frag_color;         // this encodes screen space XY data in the range 0..1

layout(location = 2) uniform vec4 iCamPosition;   // position of the view camera. `.w` is fov
layout(location = 3) uniform vec4 iMouse;         // mouse control input
layout(location = 4) uniform float iAspect;       // screen aspect ratio
layout(location = 5) uniform float iTime;        // time in seconds since start of program

out vec4 fragColor;         // final pixel output color

// Note for Shadertoy conversion: `fragCoord` should be replaced with `gl_FragCoord.xy`
// The rendering code is down at the bottom

// Ambient occlusion shading
#define AMB_OCC 1

// Cast soft shadows
#define SHADOWS 0

// raycast reflections
#define REFLECTIONS 0

// More steps = more refined shadows (if enabled)
#define SHADOW_QUALITY 16

// More step gives better quality but (potentially) lower framerates
// This mostly affects viewing angles that are shallow to a long surface
// Reasonable range is 50 to 100
#define STEPS 64

//----------------------------------------------------------------------------------------------------//
//   Object formulas
//----------------------------------------------------------------------------------------------------//

float sdPlane( vec3 p ) { return p.y; }
float sdSphere( vec3 p, float s ) { return length(p)-s; }
float sdBox( vec3 p, vec3 b ) {
    vec3 d = abs(p) - b;
    return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

float sdEllipsoid( in vec3 p, in vec3 r ) { return (length( p/r ) - 1.0) * min(min(r.x,r.y),r.z); }
float udRoundBox( vec3 p, vec3 b, float r ) { return length(max(abs(p)-b,0.0))-r; }
 float sdTorus( vec3 p, vec2 t ) { return length( vec2(length(p.xz)-t.x,p.y) )-t.y; }

float sdHexPrism( vec3 p, vec2 h )
{
    vec3 q = abs(p);
#if 0
    return max(q.z-h.y,max((q.x*0.866025+q.y*0.5),q.y)-h.x);
#else
    float d1 = q.z-h.y;
    float d2 = max((q.x*0.866025+q.y*0.5),q.y)-h.x;
    return length(max(vec2(d1,d2),0.0)) + min(max(d1,d2), 0.);
#endif
}

float sdCapsule( vec3 p, vec3 a, vec3 b, float r )
{
	vec3 pa = p-a, ba = b-a;
	float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
	return length( pa - ba*h ) - r;
}

float sdEquilateralTriangle(  in vec2 p )
{
    const float k = sqrt(3.0);
    p.x = abs(p.x) - 1.0;
    p.y = p.y + 1.0/k;
    if( p.x + k*p.y > 0.0 ) p = vec2( p.x - k*p.y, -k*p.x - p.y )/2.0;
    p.x += 2.0 - 2.0*clamp( (p.x+2.0)/2.0, 0.0, 1.0 );
    return -length(p)*sign(p.y);
}

float sdTriPrism( vec3 p, vec2 h ) {
    vec3 q = abs(p);
    float d1 = q.z-h.y;
#if 1
    // distance bound
    float d2 = max(q.x*0.866025+p.y*0.5,-p.y)-h.x*0.5;
#else
    // correct distance
    h.x *= 0.866025;
    float d2 = sdEquilateralTriangle(p.xy/h.x)*h.x;
#endif
    return length(max(vec2(d1,d2),0.0)) + min(max(d1,d2), 0.);
}

float sdCylinder( vec3 p, vec2 h ) {
    vec2 d = abs(vec2(length(p.xz),p.y)) - h;
    return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}

float sdCone( in vec3 p, in vec3 c ) {
    vec2 q = vec2( length(p.xz), p.y );
    float d1 = -q.y-c.z;
    float d2 = max( dot(q,c.xy), q.y);
    return length(max(vec2(d1,d2),0.0)) + min(max(d1,d2), 0.);
}

float sdConeSection( in vec3 p, in float h, in float r1, in float r2 ) {
    float d1 = -p.y - h;
    float q = p.y - h;
    float si = 0.5*(r1-r2)/h;
    float d2 = max( sqrt( dot(p.xz,p.xz)*(1.0-si*si)) + q*si - r2, q );
    return length(max(vec2(d1,d2),0.0)) + min(max(d1,d2), 0.);
}

float sdPryamid4(vec3 p, vec3 h ) { // h = { cos a, sin a, height }
    // Tetrahedron = Octahedron - Cube
    float box = sdBox( p - vec3(0,-2.0*h.z,0), vec3(2.0*h.z) );
 
    float d = 0.0;
    d = max( d, abs( dot(p, vec3( -h.x, h.y, 0 )) ));
    d = max( d, abs( dot(p, vec3(  h.x, h.y, 0 )) ));
    d = max( d, abs( dot(p, vec3(  0, h.y, h.x )) ));
    d = max( d, abs( dot(p, vec3(  0, h.y,-h.x )) ));
    float octa = d - h.z;
    return max(-box,octa); // Subtraction
 }

float length2( vec2 p ) {
	return sqrt( p.x*p.x + p.y*p.y );
}

float length6( vec2 p ) {
	p = p*p*p; p = p*p;
	return pow( p.x + p.y, 1.0/6.0 );
}

float length8( vec2 p ) {
	p = p*p; p = p*p; p = p*p;
	return pow( p.x + p.y, 1.0/8.0 );
}

float sdTorus82( vec3 p, vec2 t ) {
    vec2 q = vec2(length2(p.xz)-t.x,p.y);
    return length8(q)-t.y;
}

float sdTorus88( vec3 p, vec2 t ) {
    vec2 q = vec2(length8(p.xz)-t.x,p.y);
    return length8(q)-t.y;
}

float sdCylinder6( vec3 p, vec2 h ) {
    return max( length6(p.xz)-h.x, abs(p.y)-h.y );
}


//----------------------------------------------------------------------------------------------------//
//   Combining operations
//----------------------------------------------------------------------------------------------------//

// Subtraction
float opS( float d1, float d2 )
{
    return max(-d2,d1);
}

// Union
vec2 opU( vec2 d1, vec2 d2 )
{
	return (d1.x<d2.x) ? d1 : d2;
}

// repeat
vec3 opRep( vec3 p, vec3 c )
{
    return mod(p,c)-0.5*c;
}

//----------------------------------------------------------------------------------------------------//
//   Space-warping operations
//----------------------------------------------------------------------------------------------------//

// Twist
vec3 warpTwist( vec3 p, float turns, float start )
{
    float  t = 3.141592 * turns;
    float  c = cos(t*p.y+start);
    float  s = sin(t*p.y+start);
    mat2   m = mat2(c,-s,s,c);
    return vec3(m*p.xz,p.y);
}

// Spherical surface blobs
vec3 warpBlobs( vec3 p)
{
    return p + 0.04*sin(50.0*p.x)*sin(50.0*p.y)*sin(50.0*p.z);
}

// rotate around Y
vec3 warpRotY( vec3 p, float angle )
{
    float  c = cos(angle);
    float  s = sin(angle);
    mat2   m = mat2(c,-s,s,c);
    return vec3(m*p.xz,p.y);
}

// rotate around Z
vec3 warpRotZ( vec3 p, float angle )
{
    float  c = cos(angle);
    float  s = sin(angle);
    mat2   m = mat2(c,-s,s,c);
    return vec3(m*p.xy,p.z);
}

// rotate around X
vec3 warpRotX( vec3 p, float angle )
{
    float  c = cos(angle);
    float  s = sin(angle);
    mat2   m = mat2(c,-s,s,c);
    return vec3(m*p.yz,p.x);
}

//----------------------------------------------------------------------------------------------------//
//   The rendering code
//----------------------------------------------------------------------------------------------------//

// This is the actual model, it should be re-built from the outer model formula
// result: .x = distance from `pos`; .y = material ID/Value
vec2 map( in vec3 pos )
{
    // sphere on a plane
    vec2 res = opU( vec2( sdPlane(     pos), 1.0 ), vec2( sdSphere(    pos-vec3( 0.0,0.25, sin(iTime)*0.7), 0.25 ), 46.9 ) );

    // some basic shapes (note each is unioned with the model-so-far)
    res = opU( res, vec2( sdBox(       warpRotY(pos-vec3( 1.0,0.25, 0.0),iTime), vec3(0.25) ), 3.0 ) );
    res = opU( res, vec2( udRoundBox(  warpRotX(pos-vec3( 1.0,0.25, 1.0),iTime), vec3(0.15), 0.1 ), 41.0 ) );
	res = opU( res, vec2( sdTorus(     warpRotZ(pos-vec3( 0.0,0.25, 1.0),iTime), vec2(0.20,0.05) ), 25.0 ) );
    res = opU( res, vec2( sdCapsule(   pos,vec3(-1.3,0.10,-0.1), vec3(-0.8,0.50,0.2), 0.1  ), 31.9 ) );
	res = opU( res, vec2( sdTriPrism(  pos-vec3(-1.0,0.25,-1.0), vec2(0.25,0.05) ),43.5 ) );
	res = opU( res, vec2( sdCylinder(  pos-vec3( 1.0,0.30,-1.0), vec2(0.1,0.2) ), 8.0 ) );
	res = opU( res, vec2( sdCone(      pos-vec3( 0.0,0.50,-1.0), vec3(0.8,0.6,0.3) ), 55.0 ) );
	res = opU( res, vec2( sdTorus82(   pos-vec3( 0.0,0.25, 2.0), vec2(0.20,0.05) ),50.0 ) );
	res = opU( res, vec2( sdTorus88(   pos-vec3(-1.0,0.25, 2.0), vec2(0.20,0.05) ),43.0 ) );
	res = opU( res, vec2( sdCylinder6( pos-vec3( 1.0,0.30, 2.0), vec2(0.1,0.2) ), 12.0 ) );
	res = opU( res, vec2( sdHexPrism(  pos-vec3(-1.0,0.20, 1.0), vec2(0.25,0.05) ),17.0 ) );
	res = opU( res, vec2( sdPryamid4(  pos-vec3(-1.0,0.15,-2.0), vec3(0.8,0.6,0.25) ),37.0 ) );
    
    // box with sphere cut out
    res = opU( res, vec2( opS( udRoundBox(  pos-vec3(-2.0,0.2, 1.0), vec3(0.15),0.05),
	                           sdSphere(    pos-vec3(-2.0,0.2, 1.0), 0.25)), 13.0 ) );


    // gear-wheel-donut thing
    res = opU( res, vec2( opS( sdTorus82(  pos-vec3(-2.0,0.2, 0.0), vec2(0.20,0.1)),
	                           sdCylinder(  opRep( vec3(atan(pos.x+2.0,pos.z)/6.2831, pos.y, 0.02+0.5*length(pos-vec3(-2.0,0.2, 0.0))), vec3(0.05,1.0,0.05)), vec2(0.02,0.6))), 51.0 ) );
    
	
    // blobby sphere
    res = opU( res, vec2( 0.5*sdSphere(  warpBlobs(pos-vec3(-2.0,0.25,-1.0)), 0.2), 65.0 ) );

    // twisted loop
	res = opU( res, vec2( 0.5*sdTorus( warpTwist(pos-vec3(-2.0,0.25, 2.0), /*turns*/2, /*start rot*/iTime),vec2(0.20,0.05)), 46.7 ) );

    // truncated cone
    res = opU( res, vec2( sdConeSection( pos-vec3( 0.0,0.35,-2.0), 0.15, 0.2, 0.1 ), 13.67 ) );

    // flattened sphere
    res = opU( res, vec2( sdEllipsoid( pos-vec3( 1.0,0.35,-2.0), vec3(0.15, 0.2, 0.05) ), 43.17 ) );
        
    return res;
}

// Do the ray-march of the SDF function
vec2 castRay( in vec3 ro, in vec3 rd )
{
    float tmin = 0.5;  // near clip plane
    float tmax = 20.0; // far clip plane
    
    float t = tmin;
    float m = -1.0; // no material
    for( int i=0; i < STEPS; i++ ) {
	    float precis = 0.0005*t; // precision limit
	    vec2 res = map( ro+rd*t ); // get distance
        if( res.x<precis || t>tmax ) break; // close enough to surface, or too far from camera
        t += res.x; // advance distance
	    m = res.y; // set material from nearest entity.
    }

    if( t>tmax ) m=-1.0; // hit no surface, reset material
    return vec2( t, m );
}

// looks nice, but takes a lot of calculation.
float softshadow( in vec3 ro, in vec3 rd, in float mint, in float tmax )
{
	float res = 1.0;
    float t = mint;
    for( int i=0; i < SHADOW_QUALITY; i++ )
    {
		float h = map( ro + rd*t ).x;
        res = min( res, 8.0*h/t );
        t += clamp( h, 0.02, 0.10 ); // set a minimum and maximum step size
        if( h<0.001 || t>tmax ) break;
    }
    return clamp( res, 0.0, 1.0 );
}

// calculate a surface normal by inpecting the isosurface nearby
vec3 calcNormal( in vec3 pos )
{
    vec2 e = vec2(1.0,-1.0)*0.5773*0.0005;
    return normalize( e.xyy*map( pos + e.xyy ).x + 
					  e.yyx*map( pos + e.yyx ).x + 
					  e.yxy*map( pos + e.yxy ).x + 
					  e.xxx*map( pos + e.xxx ).x );
}

// Ambient occlusion guess based on model distance a few steps away from the surface
float calcAO( in vec3 pos, in vec3 nor )
{
	float occ = 0.0;    // large scale
    float sca = 1.0;    // darkness and scale
    for( int i=3; i<7; i++ )
    {
        float hr = 0.01 + 0.12*float(i)/4.0;
        vec3 aopos =  nor * hr + pos;
        float dd = map( aopos ).x;
        occ += -(dd-hr)*sca;
        sca *= 0.95;
    }
    return clamp( 1.0 - 3.0*occ, 0.0, 1.0 );    
}

// do the minimum-distance ray march, lighting and coloring
vec3 render( in vec3 ro, in vec3 rd )
{ 
    vec3 col = vec3(0.7, 0.9, 1.0) +rd.y*0.8; // color based on angle to give a foggy / chrome look
    vec2 res = castRay(ro,rd); // ray march
    float t = res.x; // distance
	float m = res.y; // material

    if( m <= -1.0 ) return col; // didn't converge on an object. Show 'sky'

    vec3 pos = ro + t*rd;
    vec3 nor = calcNormal( pos );
    
    // material (make up a color based on position)
    col = 0.45 + 0.35*sin( vec3(0.05,0.08,0.10)*(m-1.0) ); // color based on 'material' returned from `map`
    //col = nor; // color based on normal. Works nicely for CAD purposes.
    if( m <= 1.0 ) // floor level, do a checker board. Remove if objects can be directly colored
    {
        
        float f = mod( floor(5.0*pos.z) + floor(5.0*pos.x), 2.0);
        col = 0.3 + 0.1*f*vec3(1.0);
    }

    // lighting
    #if AMB_OCC
    float occ = calcAO( pos, nor ); // Ambient occlusion
    #else
    float occ = 1.0;
    #endif
    vec3  lig = normalize( vec3(-0.4, 0.7, -0.6) );
    vec3  hal = normalize( lig-rd );
    float amb = clamp( 0.5+0.5*nor.y, 0.0, 1.0 );
    float dif = clamp( dot( nor, lig ), 0.0, 1.0 );
    float bac = clamp( dot( nor, normalize(vec3(-lig.x,0.0,-lig.z))), 0.0, 1.0 )*clamp( 1.0-pos.y,0.0,1.0);
    float fre = pow( clamp(1.0+dot(nor,rd),0.0,1.0), 2.0 );
    float dom = 1.0;
    
    #if SHADOWS
    dif *= softshadow( pos, lig, 0.02, 2.5 );
    #endif
    #if REFLECTIONS
    vec3 ref = reflect( rd, nor );
    dom = smoothstep( -0.1, 0.1, ref.y );
    dom *= softshadow( pos, ref, 0.02, 2.5 );
    #endif

    // Shine
    float spe = pow( clamp( dot( nor, hal ), 0.0, 1.0 ),16.0)*
                dif *
                (0.04 + 0.96*pow( clamp(1.0+dot(hal,rd),0.0,1.0), 5.0 ));

    // Combine lighting and coloring to give a material effect
    vec3 lin = vec3(0.0);
    lin += 1.30*dif*vec3(1.00,0.80,0.55);
    lin += 0.40*amb*vec3(0.40,0.60,1.00)*occ;
    lin += 0.50*dom*vec3(0.40,0.60,1.00)*occ;
    lin += 0.50*bac*vec3(0.25,0.25,0.25)*occ;
    lin += 0.25*fre*vec3(1.00,1.00,1.00)*occ;
    col = col*lin;
    col += 10.00*spe*vec3(1.00,0.90,0.70);

    col = mix( col, vec3(0.8,0.9,1.0), 1.0-exp( -0.0002*t*t*t ) );

	return vec3( clamp(col,0.0,1.0) );
}

// origin, target, rotation
mat3 setCamera( in vec3 ro, in vec3 ta, float cr )
{
	vec3 cw = normalize(ta-ro);
	vec3 cp = vec3(sin(cr), cos(cr), 0.0);
	vec3 cu = normalize( cross(cw,cp) );
	vec3 cv = normalize( cross(cu,cw) );
    return mat3( cu, cv, cw );
}

void main()
{
    vec2 p = vec2(frag_color.x, frag_color.y / iAspect); // screen space position (from vertex shader, acts as eye ray direction)

    // camera	
    //vec3 ro = vec3( -0.5+3.5*cos(0.1*time + 6.0*mo.x), 1.0 + 2.0*mo.y, 0.5 + 4.0*sin(0.1*time + 6.0*mo.x) ); // position (Rotation Origin)
    vec3 ro = iCamPosition.xyz;
    vec3 ta = vec3( 0,0,0 ); // target
    // camera-to-world transformation
    mat3 ca = setCamera( ro, ta, 0.0 );
    // ray direction
    vec3 rd = ca * normalize( vec3(p.xy, iCamPosition.w) ); // 2.0 here is the focal length. Lower = wider angle. Higher = telephoto

    // render -- do the minimum-distance ray march, lighting and coloring
    vec3 col = render( ro, rd );

    // gamma -- correct colors
    col = pow( col, vec3(0.4545) );

    fragColor = vec4( col, 1.0 );
}