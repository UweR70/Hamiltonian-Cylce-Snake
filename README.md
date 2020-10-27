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
I have also thought about further optimizations, such as avoid drawing a playfield square/cell with a color that the cell already has. Etc.<br/>
<br/>
Take a deep dive in my code and you will find much more topics I was thinking about.<br/>
<br/>

# Deep thoughts
Just to give you an impression how deep my thoughts were.<br/>
Please notice that the following examples are representing a case 1 playfield where x and y are even; which also handles case 3, see above.<br/>
<br/>
The following image is just one example to give you the basic idea.<br/>
<br/>
Yes, at the first glance it is a little bit confusing but stick with me, take your time, and you will see ...<br/> 
<ul>
  <li>
    ... the snakehead (blue), snakebody (green) and snaketail (yellow).
  </li>
  <li>
    ... that all snake parts are in the correct Hamiltonian Cycle order.
  </li>
  <li>
    ... the here important part of the "normal" Hamiltonian Cycle.
  </li>
  <li>
     ... the brown cells that are showing the generated abbreviation path.
  </li>
</ul>
<img src="https://live.staticflickr.com/65535/50536244562_fb1632bfdd_w.jpg" style="width: 100px; height: 100px;">
The abbreviation path ends in this example in the row that contains the apple<br/> 
because the "normal" Hamiltonian Cycle will lead the snake directly to the apple.<br/>
<br/>
But wait! Doing it this way means that the snake is going to following the Hamiltonian Cycle after it eat the apple.<br/>
Means it will also walk first through cell {1; 2} and then through cell {1; 1}.<br/>
Why not spare these movements and generate for example this abbreviation path?<br/>
<br/>
<img src="https://live.staticflickr.com/65535/50536331956_1a2dac30c4_n.jpg" style="width: 100px; height: 100px;">
On the one hand, this violates the rule mentioned above:
<pre><code>
An abbreviation only makes sense if the snake can be returned to the<br/>
generated Hamiltonian Cycle immediately after processing the abbreviation.<br/>
</code></pre> 
On the other hand, the path covered by the snake should be as short as possible.<br/>
Ok, so lets try to spare these two steps menat above!<br/>
For the above shown situations its looks good but what in situations like shown in the following image?<br/>
<br/>
<img src="https://live.staticflickr.com/65535/50537260752_16e6194f0b_z.jpg""><br/>
<img src="https://live.staticflickr.com/65535/50536384398_2fce8a3915_z.jpg"><br/>
Means we have also to consider about the situation when the snake eat the apple.<br/>
Like: Is it possible to go one line up? (To stay in this example.)<br/>
<br/>
But we have also to consider about the fact that going one line up would means that we could not generate<br/>
the next abbreviation path until snake parts (head, body, tail) are in the correct order given by the generated Hamiltonian Cycle.<br/>
<br/>
<br/>
<br/>
This leads us to the colculation that the spared steps are equal to <br/>
maximum half of the cells/steps of the row which includes the apple<br/>
plus the same amount of steps for the row "above" this apple containing rowe.<br/>
(Yes, other separations are possible.)<br/>
But finally we are talking abount an amont of spared steps equal to the playfield width.<br/>
Lets call the amount of these spared steps "Amount A" steps.<br/>
<br/>
On the other hand, doing it as discussesd means that we have to wait with the next abbreviation path calculation <br/>
until all snake part "in-line". <br/>
In case snakes length is less than "Amount A" we spare steps.<br/>
In case snakes length is equal or grater than "Amount A" we loose the ability to calculate immediately the next abrreviation,<br/>
what can leed to more steps the "Amount A" step.<br/>
<br/>
Due the fact that we will have more situations where the snakes length is greater than the playfield width it is legitimate to assume<br/>
that we need less steps when we stick<br/>
with the starting situation. Means <br/>
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
