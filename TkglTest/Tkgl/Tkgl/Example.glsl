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
	
    vec2 rings = opUs( // soft join
        vec2( sdTorus82(   pos-vec3( 0.0, 0.25, 2.0), vec2(0.20,0.05) ),50.0 ),   // round ring
        vec2( sdTorus88(   pos-vec3(-0.5, 0.25, 2.0), vec2(0.20,0.05) ),43.0 ),   // squared ring
        0.1 // smoothing parameter
    );
    res = opU(res, rings);

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
