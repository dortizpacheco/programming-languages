## Ruby
Ruby es un lenguaje orientado a objetos y sigue la noción habitual de cualquier otro lenguaje orientado a objetos para definir una clase, Ruby tiene su propio modelo de objetos el cual tiene artefactos como clases, objetos, métodos de instancia, métodos de clase y métodos singleton; posee soporte para expresiones regulares, lo cual es muy útil en la coincidencia de patrones y procesamiento de texto y se pueden implementar bloques, los cuales se utilizan para implementar lambdas y cierres en Ruby.
Sin embargo, quizás la característica más apreciada de Ruby es la metaprogramación, con la cual se puede manipular el lenguaje para satisfacer las necesidades, en lugar de adaptarse al lenguaje cómo es, es por ello que la metaprogramación y los DSLs tienen una estrecha relación en el mundo Ruby. Ruby es un lenguaje altamente dinámico; puede insertar nuevos métodos en las clases en tiempo de ejecución (incluso una clase principal como Array), crear alias para los métodos existentes, e incluso definir métodos en objetos individuales (métodos Singleton). Además, tiene una rica API para la reflexión. Reflexión, también llamada introspección, simplemente significa que un programa puede examinar su estado ,su estructura y permite a un programa alterarlos a ambos. Un programa de Ruby puede configurar dinámicamente nombres de variables, invocar nombres de métodos e incluso definir nuevas clases y nuevos métodos.

### Caracteristicas distintivas del lenguaje Ruby 
La sintaxis de Ruby es orientada en la expresividad y su unidad básica es la expresión, el intérprete de Ruby evalúa las expresiones y produce valores. Las expresiones más simples son las expresiones primarias, las cuales representan directamente los valores ejemplo de esto son los números y las cadenas. Lenguaje tiene muchas similitudes con **Python**, aunque con diferencia en en pequeños detalles. Mientras que **Python** intenta a la vez ser lo mas legible y sencillo posible, Ruby intenta ser lo mas explesivo posible dentro de la sencilles y el tipado dinámico. Luego siguiendo esa idea los **scope** en Ruby estan delimitados por las palabras predefinidas del lenguage (if, def, etc) y  la palabra clave **end**. A continuacion se enumeraran alguans diferencias

  1. En Ruby Las estructuras de control son expresiones y por tanto, se puede escribir código como el siguiente: 

```ruby
    minimus = if x < t then x else t end  
```
  2. En Ruby tienes una referencia a la clase (**self**) que ya está en el cuerpo de la clase. En Python, no tiene una referencia a la clase hasta que finaliza la construcción de la clase.

```ruby
class RubyClass
  puts self
end
```

self en este caso es la clase, y este código imprimirá "RubyClass". No hay forma de imprimir el nombre de la clase o de otra manera acceder a la clase desde el cuerpo de definición de clase en Python (definiciones de método externas).

  3. En Ruby para referirse a una propiedad de una clase se realiza a partir del **@** en ves de **_self.nombre de la propiedad_**
  
  ```ruby
    class MyRubyClass
      def initialize(text)
        @text = text
      end
      def welcome
        @text
      end
    end
```

  4. Todas las clases son mutables en Ruby.
Esto le permite desarrollar extensiones para las clases principales:

```ruby
    class String
      def starts_with?(other)
        head = self[0, other.length]
        head == other
      end
    end
```

  5. Ruby tiene bloques
Con la instrucción **do**, puede crear una función anónima de varias líneas en Ruby, que se pasará como un argumento al método frente a do, y se llamará desde allí. En Python, en su lugar, haría esto ya sea pasando un método o con generadores.

```ruby
def themethod
    yield 5
end

themethod do |foo|
    puts foo
end
```
  6. Python tiene un manejo flexible del espacio de nombres
En Ruby, cuando se importa un archivo con **require**, todas las cosas definidas en ese archivo terminarán en su espacio de nombres global. Esto causa la contaminación del espacio de nombres. La solución a eso son los módulos Rubys. Pero si crea un espacio de nombres con un módulo, debe usar ese espacio de nombres para acceder a las clases contenidas.

  7. Python tiene herencia múltiple
Ruby no lo hace si no que reutiliza el concepto de módulo como un tipo de clases abstractas.

  8. Python tiene comprensiones de listas / dict
Ruby puede simularlo con:

```ruby
res = (0..9).map { |x| x * x }
```

### Ruby y DSL

En los lenguajes compilados como C++ los métodos y variables tienen valor en un espacio de memoria solo en tiempo de compilación, una vez finaliza el período de compilación los espacios de memoria se liberan y se pierde la información relacionada con el programa, por esa razón no se pueden solicitar a una clase sus métodos de instancia, ya que, en el momento de hacer la solicitud, la clase se ha desvanecido. Por otra parte, en los lenguajes interpretados como Ruby la metaprogramación es posible, ya que en tiempo de ejecución la mayoría de las construcciones del lenguaje todavía están ahí, de esa forma, se pueden consultar valores y construcciones del programa en ejecución. 

Para la metaprogramacion Ruby se vale de las clases Kernel, Object y Module definen métodos con el mismo objetivo que **_Reflection en C#_**, trabajar con los metadatos del programa en ejecución. Pero gracias al tipado dinámico de Ruby y que sus tipos prncipales no son inmutables, este trabajo con los metadatos del programa aporta una gran flexibilidad al lenguaje. A continuación algunos ejemplos:

```ruby
    def array_second
      self[1]
    end
    a = [1,2,3]
    a.instance_eval(array_second)
    a.second #returns: 2
```
```ruby
  str = "ruby.devcoop.fr"
  str.instance_eval do 
    def /(delimiter) 
      split(delimiter)
    end
  end
  str / "." # return: ["ruby" , "devcoop", "fr"]
```

Otra forma de realizar esto es mediante los metodos add_method, remove_method y de undef_method.   
Otro método útil para manipular métodos es el alias_method o simplemente alias, mediante estos métodos es posible darle sinónimos a un método existente, funcionac como un diccionario en el cual un método puede tener diferentes nombres. Por otra parte, si se llama a un método no existente, se puede usar method_missing para capturar y manejar invocaciones arbitrarias en un objeto.  

Vease la implementacion las clases person en Ruby