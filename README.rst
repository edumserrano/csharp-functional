C# Functional
=============

|docs| |licence|

Maybe Monad |maybe-monad-build-status| |maybe-monad-nuget|

|build-status| |nuget|

|build-status| |nuget|

|build-status| |nuget|

|build-status| |nuget|

|build-status| |nuget|

|build-status| |nuget|

|build-status| |nuget|



This repository started as a learning experience about functional programming concepts. The main trigger was a Pluralsight video from Vladimir Khorikov named `Applying Functional Principles in C# <https://www.pluralsight.com/courses/csharp-applying-functional-principles>`_.

Installing
=================================================

Installation is performed via NuGet::
    
    PM> Install-Package Roslyn.Analyzers

Building
=================================================

This repository adheres to the `F5 manifesto <http://www.khalidabuhakmeh.com/the-f5-manifesto-for-net-developers>`_ so you should be able to clone, open in Visual Studio and build.

Documentation
=================================================

For documentation go `here <http://csharp-functional.readthedocs.io/en/latest>`_.
To understand better the structure of this repository see the section `About the repository <http://csharp-functional.readthedocs.io/en/latest/repository.html>`_.

Licence
=================================================

This project is licensed under the `MIT license <https://github.com/edumserrano/roslyn-analyzers/blob/master/Licence>`_.


==================================================================== ============ ============
Description                                                          NuGet        Build Status
==================================================================== ============ ============
Maybe monad                                                          abc          abc
Result monad                                                         abc          abc
HttpResult monad                                                     abc          abc
Result monad extensions                                              abc          abc
HttpResult monad extensions                                          abc          abc
Result monad extensions that transforms them into HttpResult monad   abc          abc
Maybe monad extensions that them into Result monad                   abc          abc
Simple application of HttpResult monad                               abc          abc



.. |docs| image:: http://csharp-functional.readthedocs.io/en/latest/?badge=latest
    :alt: Documentation Status
    :scale: 100%
    :target: http://csharp-functional.readthedocs.io/en/latest    

.. |licence| image:: https://img.shields.io/github/license/mashape/apistatus.svg
    :alt: licence
    :scale: 100%
    :target: https://github.com/edumserrano/csharp-functional/blob/master/LICENSE

.. |maybe-monad-nuget| image:: https://img.shields.io/nuget/v/MaybeMonad.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/MaybeMonad/

.. |maybe-monad-build-status| image:: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/35/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/35/badge
