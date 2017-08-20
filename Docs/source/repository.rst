.. include:: links.rst

.. _repository:

Notes on the repository
=======================

There is one in the |repo|_:
  
  * `Source/CSharpFunctional/CSharpFunctional.sln <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional>`_

CSharpFunctional.sln
--------------------


=================================================================================================================  ============ =================================================== ============================================================================================================================================================================================================================ 
Description                                                                                                        Type         NuGet                                               Location                                                         
=================================================================================================================  ============ =================================================== ============================================================================================================================================================================================================================ 
The Maybe monad                                                                                                    Source       |MaybeNuget|_                                       `MaybeMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/MaybeMonad>`_                     
The Result monad                                                                                                   Source       |ResultNuget|_                                      `ResultMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad>`_
The HttpResult monad                                                                                               Source       |HttpResultNuget|_                                  `HttpResultMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad>`_
The Result monad extensions                                                                                        Source       |ResultMonadExtensionsNuget|_                       `ResultMonad.Extensions <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad.Extensions>`_                  
The HttpResult monad extensions                                                                                    Source       |HttpResultMonadExtensionsNuget|_                   `HttpResultMonad.Extensions <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad.Extensions>`_                                                           
The Result monad extensions that :ref:`transforms them <monad-transformations>` into HttpResult monad              Source       |ResultMonadExtensionsHttpResultMonadNuget|_        `ResultMonad.Extensions.HttpResultMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad.Extensions.HttpResultMonad>`_                                                 
The Maybe monad extensions that :ref:`transforms them <monad-transformations>` into Result monad                   Source       |MaybeMonadExtensionsResultMonadNuget|_             `MaybeMonad.Extensions.ResultMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/MaybeMonad.Extensions.ResultMonad>`_                                                
Simple application of HttpResult monad on a class by using it with |HttpClient|_                                   Source       |HttpResultOnHttpClientNuget|_                      `HttpResultMonad.HttpResultOnHttpClient <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad.HttpResultOnHttpClient>`_   
Tests for the Maybe monad                                                                                          Test         N/A                                                 `MaybeMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/MaybeMonad.Tests>`_                     
Tests for the Result monad                                                                                         Test         N/A                                                 `ResultMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Tests>`_                  
Tests for the HttpResult monad                                                                                     Test         N/A                                                 `HttpResultMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/HttpResultMonad.Tests>`_                            
Tests for the Result monad extensions                                                                              Test         N/A                                                 `ResultMonad.Extensions.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Extensions.Tests>`_                       
Tests for the HttpResult monad extensions                                                                          Test         N/A                                                 `HttpResultMonad.Extensions.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/HttpResultMonad.Extensions.Tests>`_                                                           
Tests for the Result monad extensions that :ref:`transforms them <monad-transformations>` into HttpResult monad    Test         N/A                                                 `ResultMonad.Extensions.HttpResultMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Extensions.HttpResultMonad.Tests>`_                                                
Tests for the Maybe monad extensions that :ref:`transforms them <monad-transformations>` into Result monad         Test         N/A                                                 `MaybeMonad.Extensions.ResultMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/MaybeMonad.Extensions.ResultMonad.Tests>`_                                               
Tests for the simple example of applying HttpResult monad on a class by using it with |HttpClient|_                Test         N/A                                                 `HttpResultMonad.HttpResultOnHttpClient.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/HttpResultMonad.HttpResultOnHttpClient.Tests>`_                                            
Shared code between test projects                                                                                  Test Library N/A                                                 `Tests.Shared <https://github.com/edumserrano/csharp-functional/tree/master/Tests/Tests.Shared>`_                                                          
=================================================================================================================  ============ =================================================== ============================================================================================================================================================================================================================  





The main solution with the monads, extensions and unit tests projects.

The monad projects are:

* The code for the Maybe monad is at `MaybeMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/MaybeMonad>`_.
* The code for the Result monad is at `ResultMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad>`_.
* The code for the HttpResult monad is at `HttpResultMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad>`_.

The monad extensions projects are:

* The code for the Result monad extensions is at `ResultMonad.Extensions <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad.Extensions>`_.
* The code for the HttpResult monad extensions is at `HttpResultMonad.Extensions <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad.Extensions>`_.
* The code for the Result monad extensions that :ref:`transforms them <monad-transformations>` into HttpResult monad is at `ResultMonad.Extensions.HttpResultMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad.Extensions.HttpResultMonad>`_.
* The code for the Maybe monad extensions that :ref:`transforms them <monad-transformations>` into Result monad is at `MaybeMonad.Extensions.ResultMonad <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/MaybeMonad.Extensions.ResultMonad>`_.

Simple example of applying HttpResult monad on a class by using it with `System.Net.Http.HttpClient <https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netstandard-2.0>`_ :

* The code is at `HttpResultMonad.HttpResultOnHttpClient <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad.HttpResultOnHttpClient>`_.

The test projects are:

* The code for testing HttpResult monad extensions is at `HttpResultMonad.Extensions.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/HttpResultMonad.Extensions.Tests>`_.
* The code for testing the application of HttpResult based on the HttpClient class is at `HttpResultMonad.HttpResultOnHttpClient.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/HttpResultMonad.HttpResultOnHttpClient.Tests>`_.
* The code for testing the HttpResult monad is at `HttpResultMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/HttpResultMonad.Tests>`_.
* The code for testing the Maybe monad to Result monad extensions is at `MaybeMonad.Extensions.ResultMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/MaybeMonad.Extensions.ResultMonad.Tests>`_.
* The code for testing the Maybe monad is at `MaybeMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/MaybeMonad.Tests>`_.
* The code for testing the Result monad to HttpResultMonad extensions is at `ResultMonad.Extensions.HttpResultMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Extensions.HttpResultMonad.Tests>`_.
* The code for testing the Result monad extensions is at `ResultMonad.Extensions.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Extensions.Tests>`_.
* The code for testing the result monad is at `ResultMonad.Tests <https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Tests>`_.
* Shared code between test projects is at `Tests.Shared <https://github.com/edumserrano/csharp-functional/tree/master/Tests/Tests.Shared>`_.

RoslynAnalyzersTestData.sln
---------------------------


Documentation
-------------

The documentation for the repository is at the `Docs folder <https://github.com/edumserrano/roslyn-analyzers/tree/master/Docs>`_. 

Read `this <https://docs.readthedocs.io/en/latest/getting_started.html#in-restructuredtext>`_ to understand how the documentation was created and how you can build it.

Analyzers in the repository
---------------------------

Here is the list of the analyzers present in the RoslynAnalyzers.sln.

.. note:: The analyzers created in the roslyn-analyzers repository were tested on Visual Studio 2017 15.2 on different project types:

   * .NET Framework 4.6.2
   * .NET Core 1.1
   * .NET Standard 1.4

=================================================================================================================  ============  =======================================================  =================
Name                                                                                                               Identifier    Title                                                    Default action     
=================================================================================================================  ============  =======================================================  =================
:ref:`AsyncMethodNamesShouldBeSuffixedWithAsync <async-method-names-should-be-suffixed-with-async>`                ASYNC0001     Asynchronous method names should end with Async          Warning            
:ref:`NonAsyncMethodNamesShouldNotBeSuffixedWithAsync <non-async-method-names-should-not-be-suffixed-with-async>`  ASYNC0002     Non asynchronous method names should end with Async      Warning            
:ref:`AvoidAsyncVoidMethods <avoid-async-void-methods>`                                                            ASYNC0003     Avoid void returning asynchronous method                 Warning            
:ref:`UseConfigureAwaitFalse <use-configure-await-false>`                                                          ASYNC0004     Use ConfigureAwait(false) on await expression            Warning            
:ref:`SetClassAsSealed <set-class-as-sealed>`                                                                      CLASS0001     Seal class                                               Warning            
:ref:`DefaultLabelShouldBeTheLast <default-label-should-be-the-last>`                                              ENUM0001      Default switch label                                     Warning            
:ref:`MergeSwitchSectionsWithEquivalentContent <merge-switch-sections-with-equivalent-content>`                    ENUM0002      Merge switch sections                                    Warning            
:ref:`SwitchOnEnumMustHandleAllCases <switch-on-enum-must-handle-all-cases>`                                       ENUM0003      Populate switch                                          Warning
:ref:`DoNotReturnNull <do-not-return-null>`                                                                        RETURN0001    Do not return null                                       Warning                   
=================================================================================================================  ============  =======================================================  =================