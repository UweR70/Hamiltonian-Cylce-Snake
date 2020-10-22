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
The playground is defined through width (or x) and height (or y).<br/>
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
A Hamiltonian Cylce is a path that hits <u>all</u> cells <u>exact once</u>.<br/>
<br/>
The path starts and ends in the same cell.<br/>
This is the reason why its called cycle and not path.<br/>
<br/>
From these simple rules it follows that the smallest playing field is one with the dimension 2-by-2.<br/>
<br/>
<br/>
Combining (A) and (B) results in these facts:<br/>
<ul>
  <li>
    The cases<br/>
    1 (x is even and y is even)<br/>
    and<br/>
    3 (x is odd and y is even)<br/>
    can be represented with one Hamiltonian Cycle pattern.<br/>
    <br/>
  </li>
  <li>
    The case<br/>
    2 (x is even and y is odd)<br/>
    can be represented with another Hamiltonian Cycle pattern.<br/>
    <br/>
  </li>
  <li>
    The case<br/>
    4 (x is odd and y is odd)<br/>
    can not be represented with a Hamiltonian Cycle pattern.<br/>
    (See below for a possible solution by violating the Hamiltonia Cycle rule)<br/>
    <br/>
  </li>
</ul>
