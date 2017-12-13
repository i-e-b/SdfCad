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
