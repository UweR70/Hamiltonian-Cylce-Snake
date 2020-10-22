# Hamiltonian-Cylce-Snake

# Story:
This is all about the game Snake (https://en.wikipedia.org/wiki/Snake_(video_game_genre)),<br/>
Hamilitonian Cycles (https://en.wikipedia.org/wiki/Hamiltonian_path)<br/>, and this YouTube(r) video<br/>
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


- 2-dimensional playground, with a width (x) and a height (y).
- x and y values can be equal or not, reflecting a square or a rectangle.
- Conslusion: The playground/square/rectangle is splitted in so called cells, define through pairs of x and y values.
- Four cases rose: 
-- 1) x is even, y is even
-- 2) x is even, y is odd
-- 3) x is odd, y is even
-- 4) x is odd, y is odd

Hamiltonian Cylce in this enviroment:
<ul>
  <li>A path that hits all cells</li>
  <li>exact once.</li>
  <li>The path starts and ends in the same cell. This is the reason why its called cycle and not path.</li>
  <li></li>
</ul>
<br/>
