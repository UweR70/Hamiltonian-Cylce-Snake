# Hamiltonian-Cylce-Snake

# Story
This is all about the game Snake (https://en.wikipedia.org/wiki/Snake_(video_game_genre)),<br/>
Hamiltonian Cycles (https://en.wikipedia.org/wiki/Hamiltonian_path), and this YouTube<sup>®</sup> video<br/>
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
A Hamiltonian Cylce is a cycle that hits all cells exact once.<br/>
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
    <br/>
    <img src="https://live.staticflickr.com/65535/50517537862_ff6ba5b660_n.jpg" style="width: 100px; height: 100px;"><br/>
    <br/>
  </li>
</ul>
The easy part was to develop a functionality that returns a Hamilton cycle and make a snake follow the circle while eating apples.<br/>
In comparison, it was more difficult to develop a abbreviation functionality.<br/>
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
Finally contains its only few if / else statements.<br/>
At run time is the most time consuming part the "query logic" which checks whether all the snake parts are in the correct order of the Hamilton Cycle.<br/>
<br/>
To make not only this test as quick as possible, I introduced some classes which are providing everything needed.<br/>
I've also thought about further optimizations.<br/>
For example, playfield squares/cells are only drawn if the current color differs from the one to be drawn.<br/>
<br/>
Take a deep dive in my code and you will find much more optimization topics.<br/>
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
But wait! When done this way, the snake will follow the Hamiltonian Cycle after eating the apple.<br/>
<br/>
Means it will walk first through cell {1; 2} and then through cell {1; 1}.<br/>
Why not spare these movements and generate this abbreviation path, for example?<br/>
<br/>
<img src="https://live.staticflickr.com/65535/50536331956_1a2dac30c4_n.jpg" style="width: 100px; height: 100px;">
On the one hand, this violates the rule mentioned above ...
<pre><code>
An abbreviation only makes sense if the snake can be returned to the<br/>
generated Hamiltonian Cycle immediately after processing the abbreviation.<br/>
</code></pre> 
... on the other hand, the path covered by the snake should be as short as possible.<br/>
Ok, so let's try to spare these two steps meant above!<br/>
It looks fine for the situations shown above, but what about situations like the one shown in the following figure?<br/>
<br/>
<img src="https://live.staticflickr.com/65535/50537260752_16e6194f0b_z.jpg""><br/>
<img src="https://live.staticflickr.com/65535/50536384398_2fce8a3915_z.jpg">
Means we have also to consider about the situation when the snake eat the apple.<br/>
Like: Is it possible to go one row up? Or are there still parts of the snake?<br/>
(To stay in this example.)<br/>
<br/>
But we also have to take into account that going up one line would mean that we could only generate<br/>
the next abbreviation path when all snake parts (head, body, tail) are in the order specified by the<br/>
generated Hamilton Cycle.<br/>
<br/>
This leads us to the calculation that the maximum number of spared steps are equal to<br/>
<ul>
  <li>
    the number of columns<br/>
    minus one for the first column<br/>
    minus one for the cell that contains the apple
  </li>
</ul>
plus<br/>
<br/>
<ul>
  <li>
    the number of columns<br/>
    minus one for the first column<br/>
    minus one for the cell that contains the snake head.
  </li>
</ul>
<br/>
Finally we are talking about an amount of spared steps equal to:<br/>
Two times the playfield width minus four.<br/>
(Please notice that this is a approximation - but I believe a very accurate one.)<br/>
<br/>
Lets call the amount of these spared steps "Amount Spared Steps".<br/>
<br/>
On the other hand, doing it as discussed means that we have to wait with<br/>
the next abbreviation path calculation until all snake parts are "in-line".<br/>
<br/>
In case snakes length is<br/>
<ul>
  <li>
    less than "Amount Spared Steps"<br/>
    we spare steps.
  </li>
  <li>
    equal or grater than "Amount Spared Steps"<br/>
    we loose the ability to calculate immediately the next abbreviation,<br/>
    what can lead to more steps then we spare steps as mentioned in "Amount Spared Steps".
  </li>
</ul>
Due the fact that we will have more situations where the snakes length<br/>
is greater than two times the playfield width (minus four)<br/> 
it is legitimate to assume that we need less steps when we stick with the starting situation:<br/>
<br/>
<img src="https://live.staticflickr.com/65535/50536244562_fb1632bfdd_w.jpg" style="width: 100px; height: 100px;">                              
Believe me, this was just one excursion into one of many scenarios.<br/>
<br/>

# Remarks
<ul>
  <li>
    case 1 and 3:<br/>
    Fully developed. Uses Hamiltonian Cycle and abbreviation functionality.
  </li>
  <li>
    case 2:<br/>
     Fully developed. Uses Hamiltonian Cycle and abbreviation functionality.
  </li>
  <li>
    case 4:<br/>
    Nothing to develop because for this case is no Hamiltonian Cycle possible.
  </li>
  <li>
    <h3>For sure there are much more optimizations (also in the abbreviation functionality) possible!</h3><br/>
  </li>
</ul>

# Last but not least<br/>
Enjoy<br/>
