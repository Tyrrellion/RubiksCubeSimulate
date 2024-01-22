
Run commands for the app like so, "front c" for front face clockwise or "back a" for back face anti clockwise. To print the cube, type "print". To end the app type "n".
I have represented my cube differently in 2D space although the cube is accurately represented. Mine is like this in it's 2D representation:

              r r r
              r r r
              r r r
        b b b w w w g g g y y y
        b b b w w w g g g y y y
        b b b w w w g g g y y y
              o o o
              o o o
              o o o

Althought this looks different to the one on the site which is:

             w w w  
             w w w
             w w w
       o o o g g g r r r b b b
       o o o g g g r r r b b b
       o o o g g g r r r b b b
             y y y
             y y y
             y y y

Once you run the operation you requested to test, you will see that the pattern is the same.

F, R', U, B', L, D'

is the same as "front c", "right a", "top c", "back a", "left c", "bottom a"

you will see the result is:

           g b w
           y r r
           y y y
     w o o b g g o y b o y r
     b b w b w r g g r b y o
     y w b r r r b o g o o r
           w w y
           g o g
           g w w

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

THOUGHT PROCESS:

              r r r
              r r r
              r r r
        b b b w w w g g g y y y
        b b b w w w g g g y y y
        b b b w w w g g g y y y
              o o o
              o o o
              o o o

first thought on how to approach:

white clockwise = red face bottom row goes to green face left column goes to orange face top row goes to blue face right column.
blue clockwise = white face left column goes to orange face left column goes to yellow face right column goes to red face left column.
green clockwise = white face right column goes to red face right column goes to yellow face left column goes to orange face right column.

this method seemed long and tedious, after some consideration on the structure above I came up with a different approach:

New and improved rotate and shift method:

orange clockwise = blue, white, green, yellow bottom row shift right 3 spaces.
orange anticlockwise = blue, white, green, yellow bottom row left right 3 spaces.

red clockwise = blue, white, green, yellow top row shift left 3 spaces.
red anticlockwise = blue, white, green, yellow top row shift right 3 spaces.

blue clockwise = rotate yellow 180. red, white, orange, yellow shift left column down 3 spaces. de-rotate.
blue anticlockwise = rotate yellow 180. red, white, orane, yellow shift left column up 3 spaces. de-rotate.

green clockwise = rotate yellow 180. red, white, orange, yellow shift right column up 3 spaces. de-rotate.
green anticlockwise = rotate yellow 180. red, white, orange, yellow shift right column down 3 spaces. de-rotate.

white clockwise = rotate blue clockwise 90, rotate green anticlockwise 90, rotate orrange 180. blue left, red top, green right, orrange bottom shift bottom row 3 spaces right. de-rotate.
white L = same but shift left 3 spaces. de-rotate.

yellow clockwise = rotate blue 90 clockwise, rotate orrange 180, rotate green 90 anticlockwise. blue, red, green, orange, shift top row left 3 spaces. de-rotate.
yellow anticlockwise = same but shift right 3 spaces. de-rotate.

idk if there is a better solution but this seems simpler to me from a coding perspective and allows reuse.

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


