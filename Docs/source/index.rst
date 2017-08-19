.. include:: links.rst

Welcome to roslyn-analyzers documentation
=================================================

The |repo|_ started as a learning experience about functional programming concepts.

This documentation contains generic information regarding the Roslyn Platform that will help understand how the repository was created as well as convey the the information that is required to understand the code in the repository. In addition it contains a description of all the analyzers and code fixes present in the repository.


apesar de falar de monads eu n percebo nd disto se quiserem saber mais investiguem . monads esta fora do scope desta documentacao.
uma das melhores definicoes de monad vem do guia de rx "insert quote". isto em conjunto com a serie de blog posts de eric lippert (inserir como link para os tips and resources) ajudou me a entender um pouco o conceito de monads.

all code examples are meant to illustrate the usage of the functional types included in <nugets list> and are not meant to reflect real world code practices

the code examples shown might not be applicable to all domains. This might work perfectly in one project and poorly in another.


colocar uma nota algures a dizer k result tem issuccess e isfailure que sao inversos e que quando digo ok result significa result com isssuccess=true e fail result significa isfailure=true


explicar algures (talvez no extensios.rst) que as as extensoes precisam de uma func para poder transformar um tipo e que se fosse para transformar N tipos precisaria de N funcoes.
e dar um exemplo


extensions.rest notes


There can be many overloads for OnSuccess which can only be distinguished by the returning type and in C# we can not have two methods with equal signature (the return type is not part of a method's signature). This means that the only way is to change the method name to cater different return types. Therefore you find different OnSuccess methods with appended ToX where X relates to the return type.

eplicar que a motivacao veio do video da pluralsight mas com a ideia dos erros poderem ser qualquer tipo e nao so strings


ver o video da pluralsight e ler os blog posts do eric lippert pk sao key para perceber esta documentacao/.

.. toctree::
   :maxdepth: 2
   :caption: Contents:

   maybe-monad
   result-monad
   http-result-monad
   combine
   extensions
   tips-and-resources
   repository
