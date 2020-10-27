# Hamiltonian-Cylce-Snake

# Story
This is all about the game Snake (https://en.wikipedia.org/wiki/Snake_(video_game_genre)),<br/>
Hamilitonian Cycles (https://en.wikipedia.org/wiki/Hamiltonian_path), and this YouTube(r) video<br/>
<br/>
I Created a PERFECT SNAKE A.I.<br/>
https://www.youtube.com/watch?v=tjQIO1rqTBE<br/>
<br/>
However, my thought was that the Hamilton cycle ... <br/>
<br/>
<img src="https://live.staticflickr.com/65535/50508327758_f8a6e273ee_k.jpg" style="width: 100px; height: 100px;"><br/>
<br/>
... shown in the aforementioned video could be made much smoother, which should certainly lead to a much more effective path optimization.<br/>
<br/>
So the seed were set ... and I started this project.<br/>
<br/>

# Thoughts
(A)<br/>
The play field is defined through width (or x) and height (or y).<br/>
There are four cases:<br/>

<table style="width:10px; border: 1px red solid;">
  <tr>
    <th>case</th>
    <th>x</th>
    <th>y</th> 
  </tr>
  <tr>
    <td>1</td>
    <td>even</td>
    <td>even</td>
  </tr>
  <tr>
    <td>2</td>
    <td>even</td>
    <td>odd</td>
  </tr>
  <tr>
    <td>3</td>
    <td>odd</td>
    <td>even</td>
  </tr>
  <tr>
  	<td>4</td>
    <td>odd</td>
    <td>odd</td>
   </tr>
</table>
<br/>

(B)<br/>
A Hamiltonian Cylce is a cacle that hits all cells exact once.<br/>
The path starts and ends in the same cell.<br/>
<br/>
From these simple rules it follows that the smallest playing field is one with the dimension 2-by-2.<br/>
<br/>
<br/>
Combining (A) and (B) results in these facts:<br/>
(Notice that the following images are showing just some examples to give you the idea.)<br/>
<ul>
  <li>
    The cases<br/>
    1 (x is even and y is even)<br/>
    and<br/>
    3 (x is odd and y is even)<br/>
    can be represented with one Hamiltonian Cycle pattern.<br/>
    <br/>
    <img src="https://live.staticflickr.com/65535/50516580278_b87d37b579_c.jpg" style="width: 100px; height: 100px;"><br/>
    <br/>
  </li>
  <li>
    The case<br/>
    2 (x is even and y is odd)<br/>
    can be represented with another Hamiltonian Cycle pattern.<br/>
    <br/>
    <img src="https://live.staticflickr.com/65535/50533997831_fb870bd208_w.jpg" style="width: 100px; height: 100px;"><br/>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Notice: Some (not all) repeating pattern are highlighted by adding bold borders.<br/>
    <br/>
  </li>
  <li>
    The case<br/>
    4 (x is odd and y is odd)<br/>
    can not be represented with a Hamiltonian Cycle pattern.<br/>
    (See below for a possible solution by violating the Hamiltonia Cycle rule)<br/>
    <br/>
    <img src="https://live.staticflickr.com/65535/50517537862_ff6ba5b660_n.jpg" style="width: 100px; height: 100px;"><br/>
    <br/>
  </li>
</ul>
The easy part was to develop a functionality that returns a Hamilton cycle and make a snake follow the circle while eating apples.<br/>
In comparison, it was more difficult to develop a shortcut functionality.<br/>
<br/>
Top level keywords how it works:<br/>

<ul>
  <li>
    The generated Hamiltonian Cycle is used if an abbreviation cannot be calculated or an abbreviation should not be used.
  </li>
  <li>
    An abbreviation only makes sense if the snake can be returned to the generated Hamiltonian Cycle immediately after processing the abbreviation.
  </li>
  <li>
    Generate a abbreviation path only in case all snake parts (head, body, tail) are currently in the correct order given by the generated Hamiltonian Cycle.<br/>
    (The real, interlectual work is in this sentence and the reason why I wrote above "... or an abbreviation should not be used".)
  </li>
</ul>
<br/>
Surprisingly is the resulting "ShortCut" class very compact, reliable and fast.<br/>
Finally contains its only some if / else statements.<br/>
The most time consuming part is the above meant "query logic" which tests whether all snake parts are in the correct Hamiltonian Cycle order.<br/>
<br/>
To make not only this test as quick as possible, I introduced some classes which are providing everything needed.<br/>
I have also thought about further optimizations, such as avoid drawing a playfield square/cell with a color that the cell already has.Etc.<br/>
<br/>
Take a deep dive in my code and you will find much more topics I was thinking about.<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
The following image is just one example to give you the idea.<br/>
(Please notice that this example represents case 1 playfield where x and y are even).<br/>
<br/>
Yes, at a first glance it is a little bit confusing.<br/>
Take your time and you will see ...<br/> 
<ul>
  <li>
    ... the snakehead (blue), snakebody (green) and snaketail (yellow).
  </li>
  <li>
    ... that all snake parts are in the correct Hamiltonian Cycle order.
  </li>
  <li>
    ... the brown cells that are showing the generated abbreviation path.
  </li>
  <li>
     ... the here important part of the "normal" Hamiltonian Cycle.
  </li>
</ul>
In this example ends the path in the row that contains the apple because the "normal" Hamiltonian Cycle will lead the snake directly to the apple.<br/>


<br/>
<img src="https://live.staticflickr.com/65535/50536244562_fb1632bfdd_w.jpg" style="width: 100px; height: 100px;"><br/>
<br/>
<br/>
<br/>
