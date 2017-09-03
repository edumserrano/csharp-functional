C# Functional
=============

|docs| |licence|

==================================================================== ======================================= ================================================
Description                                                          NuGet                                   Build Status
==================================================================== ======================================= ================================================
Maybe monad                                                          |maybe-nuget|                           |maybe-build-status|
Result monad                                                         |result-nuget|                          |result-build-status|
HttpResult monad                                                     |http-result-nuget|                     |http-result-build-status|
Result monad extensions                                              |result-extensions-nuget|               |result-extensions-build-status|
HttpResult monad extensions                                          |http-result-extensions-nuget|          |http-result-extensions-build-status|
Result monad extensions that transforms them into HttpResult monad   |result-extensions-http-result-nuget|   |result-extensions-http-result-build-status|
Maybe monad extensions that them into Result monad                   |maybe-extensions-result-nuget|         |maybe-extensions-result-build-status|
Simple application of HttpResult monad                               |http-result-on-http-client-nuget|      |http-result-on-http-client-build-status|
==================================================================== ======================================= ================================================

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

.. |docs| image:: http://csharp-functional.readthedocs.io/en/latest/?badge=latest
    :alt: Documentation Status
    :scale: 100%
    :target: http://csharp-functional.readthedocs.io/en/latest    

.. |licence| image:: https://img.shields.io/github/license/mashape/apistatus.svg
    :alt: licence
    :scale: 100%
    :target: https://github.com/edumserrano/csharp-functional/blob/master/LICENSE

.. |maybe-nuget| image:: https://img.shields.io/nuget/v/MaybeMonad.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/MaybeMonad/

.. |result-nuget| image:: https://img.shields.io/nuget/v/ResultMonad.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/ResultMonad/

.. |http-result-nuget| image:: https://img.shields.io/nuget/v/HttpResultMonad.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/HttpResultMonad/

.. |result-extensions-nuget| image:: https://img.shields.io/nuget/v/ResultMonad.Extensions.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/ResultMonad.Extensions/

.. |http-result-extensions-nuget| image:: https://img.shields.io/nuget/v/HttpResultMonad.Extensions.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/HttpResultMonad.Extensions/

.. |result-extensions-http-result-nuget| image:: https://img.shields.io/nuget/v/ResultMonad.Extensions.HttpResultMonad.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/ResultMonad.Extensions.HttpResultMonad/

.. |maybe-extensions-result-nuget| image:: https://img.shields.io/nuget/v/MaybeMonad.Extensions.ResultMonad.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/MaybeMonad.Extensions.ResultMonad/

.. |http-result-on-http-client-nuget| image:: https://img.shields.io/nuget/v/HttpResultMonad.HttpResultOnHttpClient.svg?style=flat
    :alt: nuget package
    :scale: 100%
    :target: https://www.nuget.org/packages/HttpResultMonad.HttpResultOnHttpClient/

.. |maybe-build-status| image:: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/35/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/35/badge

.. |result-build-status| image:: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/40/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/40/badge

.. |http-result-build-status| image:: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/43/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/43/badge

.. |result-extensions-build-status| image:: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/41/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/41/badge

.. |http-result-extensions-build-status| image:: hhttps://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/44/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/44/badge

.. |result-extensions-http-result-build-status| image:: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/42/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/42/badge

.. |maybe-extensions-result-build-status| image:: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/38/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/38/badge

.. |http-result-on-http-client-build-status| image:: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/45/badge
    :alt: build status
    :scale: 100%
    :target: https://eduardomserrano.visualstudio.com/_apis/public/build/definitions/19e4afb6-184b-4d8b-a0e5-a108602592b9/45/badge
