# Domain Specific Languages

## Introduccion

Ilustrar el problema. Proponer una solucion clasica. Proponer un dsl. Porque
esto nos fue mejor?

Hablar del modelo semantico. Pensar en externo e interno. Compilarlo, generar
codigo vs interpretar. Language workbenches.

## Que es un DSL ???

Puede definirse un **DSL** como:

> Un lenguaje de programacion de expresividad limitada, enfocado a un dominio
> especifico.

Analicemos la definicion anterior:

- **Lenguaje de programacion:**  
  Un **DSL** es utilizado para dar instrucciones a una computadora. Como
  cualquier otro lenguaje de programacion, debe proveer una manera _legible_
  para los seres humanos de expresar lo que se desea, y aun asi premitir su
  ejecucion en una computadora.

- **Naturaleza de lenguaje:**  
  Como lenguajes de programacion, debe proveer fluidez, la expresividad no
  debe solo venir de las expresiones individuales, sino de la posibilidad de
  componer las mismas

- **Expresividad limitada:**  
  Un lenguaje de programacion de proposito general posee estructuras para
  controlar el flujo del programa y varias instrucciones complejas, estas
  pueden ser muy utiles, pero esto convierte al lenguaje en algo complejo de
  aprender, usar y entender. Un **DSL** por otro lado es facil de comprender,
  es legible, soporta solo lo necesario para ser utilizado en su dominio. No
  es posible contruir toda una aplicacion con un **DSL**, se construyen
  componentes de la misma. No es turing-completo en la mayoria de los casos.

- **Enfocado a un dominio especifico:**  
  Intuitivamente un lenguaje tan limitado solo puede ser util si conoce bien
  el dominio en el que sera aplicado. Este enfoque en un dominio especifico
  es lo que hace que un **DSL** tenga sentido ser usado.

Podemos dividir los **DSL** en tres grupos, de acuerdo a su naturaleza.

1. **DSLs externos:**  
   Un **DSL Externo** es un lenguaje diferente al de la aplicacion principal,
   generalmente con una sintaxis propia, aunque en ocaciones pueden
   utilizarse algunas existentes como _xml_, _json_, etc. Un script en un
   _dsl externo_ usualmente es parseado por la aplicacion principal.

2. **DSLs internos**:  
   Un **DSL Interno** es una manera particular de utilizar un lenguaje de
   proposito general. Un script en un _dsl interno_ es valido tambien en el
   lenguaje de proposito general, pero solamente utiliza un subconjunto del
   mismo en un estilo determinado para un aspecto del sistema general. El
   resultado se siente como un lenguaje diferente. **LISP** y **RUBY** son
   ejemplos importantes aqui.

3. **Language Workbenches:**  
   Un **Language Workbench** es un _IDE_ especializado para la creacion de
   **DSLs**. En particular un **Language Workbench** no solo es utilizado
   para determinar la estructura del **DSL** sino para editar scripts de este
   **DSL**. EL _script_ resultante intimamente combina el _IDE_ con el
   _**DSL**_

Puede pensarse en un **DSL** como un frontend o interfaz a una biblioteca o
modelo. De ahi la importancia de los **Semantic Models**.

## Fronteras de los DSL:

Es facil ver que identificar si estamos en presencia de un **DSL** o no, es
algo que puede llegar a ser ambiguo, la frontera entre lo que es un **DSL** y
lo que no lo es es algo borrosa en muchos casos. Tratemos de definir mejor
esta frontera para cada tipo de **DSL**.

Para los **DSLs internos** esta frontera la define la expresividad limitada.
La diferencia aqui se establece entre el **DSL interno** y una _command query
api_. Una manera comun de documentar una _clase_ en una _api clasica_ es
listar los metodos que esta contiene y documentarlos uno a uno, estos metodos
tienen que tener sentido por si solos, ser _autocontenidos_; por otro lado en
un **DSL interno** podria pensarse en los metodos como palabras de un
lenguaje que no expresan una idea hasta que no son compuestas en oraciones.
Por ejemplo metodos `transition(event)` o `to(dest)` no tendrian sentido por
si solo en una _api clasica_, pero pueden ser utilizados en un **DSL
interno** de la siguiente manera `.transition(event).to(dest)`. Es comun
tambien destinguir los **DSLs internos** por la no utilizacion de
condicionales, ciclos, o cualquier otra estructura de control de flujo.

Para los **DSLs externos** la frontera se establece con los lenguajes de
proposito general. Ser enfocados a un dominio no convierte a un lenguaje en
un **DSL**, dado que a pesar de su enfoque especifico, puede ser utilizado
para tareas de proposito general, por ejemplo lenguaes como **R**. Otro
ejemplo de un **DSL externo** son las expresiones regulares, estas definen un
lenguaje enfocado a hacer _text matching_, y ademas de tener este enfoque
especifico, tienen un lenguaje lo suficientemente _limitado_ como para lograr
solo la expresividad necesaria para resolver problemas de este dominio. Es
comun distinguir en los **DSL** que no sean _Turing completos_. Los archivos
de configuracion (_.json_, _.xml_, _etc_) pueden considerarse **DSL**, pero
solo en los casos en los que tienen el objetivo de ser no solo entendibles
por un ser humano, sino de ser editables.

## Ventajas de un DSL

- **Aumento de la productividad:**  
  Menos bugs, mas facil de encontrarlos. Menos duplicacion de codigo, gracias
  a modelo semantico.
- **Comunicacion con expertos del dominio**
- **Mayor expresividad, el codigo tiene mayor semantica**
- **Modelo computacional alternativo:**  
  En ocaciones una maquina de estado o expresiones regulares, etc pueden ser
  una mejor manera de lograr lo que se desea que la clasica programacion
  imperativa, donde tenemos que tener en cuenta cada detalle para tener el
  funcionamiento correcto de nuestro programa. En este caso el
  _modelo semantico_ representa esta forma computacional alternativa,
  mientras que un **DSL** permite instanciar y _populate_ este modelo de
  manera comoda, con todas las ventajas que hemos visto hasta ahora.

## Desventajas

- **Cacofonia del lenguaje:**  
  El problema mas comun con los **DSL** es la gran cantidad de **DSL** que
  pueden llegar a existir, lo cual hace dificil que la gente los aprenda; y
  como al traer gente nueva a un proyecto, puede tomar tiempo que los nuevos
  trabajadores se adapten a los **DSL** que en este son usados.

## TODO

- Important: Semantic Model == AST ???
- Ver: Implementing DSL parser workings
