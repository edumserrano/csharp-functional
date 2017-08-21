.. include:: links.rst

.. _repository:

Notes on the repository
=======================

There is only one sln in the |repo|_:
  
* `Source/CSharpFunctional/CSharpFunctional.sln <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional>`_

CSharpFunctional.sln
--------------------

.. _sln-structure:

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

Documentation
-------------

The documentation for the repository can be found at the `Docs folder <https://github.com/edumserrano/csharp-functional/tree/master/Docs>`_. 

Read `this <https://docs.readthedocs.io/en/latest/getting_started.html#in-restructuredtext>`_ to understand how the documentation was created and how you can build it.
