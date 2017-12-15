# SdfCad
CAD program for 3D printing using a signed-distance-field as the model

This is currently entirely experimental.

## Plans:

* Use a simple language to define the SDF, with subroutines and a library of joining functions
* Preview with a ray-march algorithm?
* Use my old tetrahedron surface sampler to export an SDF file
* When exporting to SDF, include 3D printing features, like:
  - different resolutions in X,Y,Z;
  - different minimum feature size in X,Y,Z;
  - holes can be smaller than fill (i.e. inside and outside have different minimum feature size).

## Possible references

Articles:

https://www.codeproject.com/Articles/1167212/OpenGL-with-OpenTK-in-Csharp-Part-Initialize-the-G

Similar project:

https://github.com/movAX13h/Zwerg

STL Files:

https://github.com/QuantumConcepts/STLdotNET
http://frankniemeyer.blogspot.co.uk/2014/05/binary-stl-io-using-nativeinteropstream.html

SDF modelling and rendering

https://github.com/gokselgoktas/sdf-ray-marching
http://www.iquilezles.org/www/articles/distfunctions/distfunctions.htm
http://www.iquilezles.org/www/articles/raymarchingdf/raymarchingdf.htm
http://jamie-wong.com/2016/07/15/ray-marching-signed-distance-functions/
http://flafla2.github.io/2016/10/01/raymarching.html

Basic language?

https://github.com/i-e-b/DBSS
