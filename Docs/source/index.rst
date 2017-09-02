.. include:: links.rst

Welcome to csharp-functional documentation
==========================================

The |repo|_ started as a learning experience about functional programming concepts. The main trigger was a Pluralsight video from `Vladimir Khorikov <http://enterprisecraftsmanship.com/>`_ named `Applying Functional Principles in C# <https://app.pluralsight.com/library/courses/csharp-applying-functional-principles/table-of-contents>`_. 

After watching the video I immediately tried to apply the concepts in one of my pet projects and I found that I wanted a bit more than the functionality described in the Pluralsight course. In the course the Result type that is described is capable of holding or not a value, so you have: Result or Result<T>. In both cases you have an error property which is of type string that you can chose to set to describe what went wrong. I felt that using a string to describe an error was not what I wanted in most cases. So all of this started because I wanted to create a Result monad which could have an error of any type. It turned out to be much more complex than I could have imagined... 

I highly advise you to watch that Pluralsight course as well as to read `Eric Lippert's series of blog posts on monads <https://ericlippert.com/category/monads/>`_. 

This documentation aims to provide enough instructions to successfully use the :ref:`NuGet packages <sln-structure>` as well as understanding the code in the repository. While reading it keep in mind that:

* Although it mentions monads, it's outside of the scope to try to explain what a monad is. 
* The :ref:`more resources <more-resources>` section contains links that will be helpful to understand what a monad is.
* Since my understanding of monads is limited it might very well be possible that I sometimes use the word incorrectly.
* All code examples are meant to illustrate the usage of the :ref:`NuGet packages <sln-structure>` and are not meant to reflect real world code practices.
* Whenever I say an ok result/httpresult I mean a result/httpresult that has the IsSuccess property equal to true.
* Whenever I say a fail result/httpresult I mean a result/httpresult that has the IsFailure property equal to true.

.. toctree::
   :maxdepth: 2
   :caption: Contents:

   repository
   maybe-monad
   result-monad
   http-result-monad
   combine
   extensions
   tips-and-resources
