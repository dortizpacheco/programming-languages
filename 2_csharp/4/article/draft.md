# Domain Specific Languages

## Introduccion

Ilustrar el problema. Proponer una solucion clasica. Proponer un dsl. Porque esto nos fue mejor?

Hablar del modelo semantico. Pensar en externo e interno. Compilarlo, generar codigo vs interpretar. Language workbenches.

## Que es un DSL ?

Puede definirse un **DSL** como:

> Un lenguaje de programacion de expresividad limitada, enfocado a un dominio especifico.

Analicemos la definicion anterior:

- **Lenguaje de programacion:**  
  Un **DSL** es utilizado para dar instrucciones a una computadora. Como cualquier otro lenguaje de programacion, debe proveer una manera _legible_ para los seres humanos de expresar lo que se desea, y aun asi premitir su ejecucion en una computadora.

- **Naturaleza de lenguaje:**  
  Como lenguajes de programacion, debe proveer fluidez, la expresividad no debe solo venir de las expresiones individuales, sino de la posibilidad de componer las mismas

- **Expresividad limitada:**  
  Un lenguaje de programacion de proposito general posee estructuras para controlar el flujo del programa y varias instrucciones complejas, estas pueden ser muy utiles, pero esto convierte al lenguaje en algo complejo de aprender, usar y entender. Un **DSL** por otro lado es facil de comprender, es legible, soporta solo lo necesario para ser utilizado en su dominio. No es posible contruir toda una aplicacion con un **DSL**, se construyen componentes de la misma. No es turing-completo en la mayoria de los casos.

- **Enfocado a un dominio especifico:**  
  Intuitivamente un lenguaje tan limitado solo puede ser util si conoce bien el dominio en el que sera aplicado. Este enfoque en un dominio especifico es lo que hace que un **DSL** tenga sentido ser usado.

Podemos dividir los **DSL** en tres grupos, de acuerdo a su naturaleza.

1. **DSLs externos:**  
   Un **DSL Externo** es un lenguaje diferente al de la aplicacion principal, generalmente con una sintaxis propia, aunque en ocaciones pueden utilizarse algunas existentes como _xml_, _json_, etc. Un script en un _dsl externo_ usualmente es parseado por la aplicacion principal.

2. **DSLs internos**:  
   Un **DSL Interno** es una manera particular de utilizar un lenguaje de proposito general. Un script en un _dsl interno_ es valido tambien en el lenguaje de proposito general, pero solamente utiliza un subconjunto del mismo en un estilo determinado para un aspecto del sistema general. El resultado se siente como un lenguaje diferente. **LISP** y **RUBY** son ejemplos importantes aqui.

3. **Language Workbenches:**  
   Un **Language Workbench** es un _IDE_ especializado para la creacion de **DSLs**. En particular un **Language Workbench** no solo es utilizado para determinar la estructura del **DSL** sino para editar scripts de este **DSL**. EL _script_ resultante intimamente combina el _IDE_ con el _**DSL**_

Puede pensarse en un **DSL** como un frontend o interfaz a una biblioteca o modelo. De ahi la importancia de los **Semantic Models**.

## Fronteras de los DSL:

Es facil ver que identificar si estamos en presencia de un **DSL** o no, es algo que puede llegar a ser ambiguo, la frontera entre lo que es un **DSL** y lo que no lo es es algo borrosa en muchos casos. Tratemos de definir mejor esta frontera para cada tipo de **DSL**.

Para los **DSLs internos** esta frontera la define la expresividad limitada. La diferencia aqui se establece entre el **DSL interno** y una _command query api_
