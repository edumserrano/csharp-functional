.. _repository:

Notes on the repository
=======================

There is one in the |repo|_:
  
  * `Source/CSharpFunctional/CSharpFunctional.sln <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional>`_

CSharpFunctional.sln
--------------------

The main solution with the monads, extensions and unit tests projects.

The monad projects are:

* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/MaybeMonad>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad>`_.

The monad extensions projects are:

* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad.Extensions>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad.Extensions>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/ResultMonad.Extensions.HttpResultMonad>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/MaybeMonad.Extensions.ResultMonad>`_.

Example of applying HttpResult monad on a class that does http calls (HttpClient):

* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional/HttpResultMonad.HttpResultClient>`_.

The test projects are:

* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Tests/HttpResultMonad.Extensions.Tests>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Tests/HttpResultMonad.Tests>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Tests/MaybeMonad.Extensions.ResultMonad.Tests>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Tests/MaybeMonad.Tests>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Extensions.HttpResultMonad.Tests>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Extensions.Tests>`_.
* The code for the analyzers and code fix providers is at `https://github.com/edumserrano/csharp-functional/tree/master/Tests/ResultMonad.Tests>`_.
* The code for the analyzers and code fix providers is at `zxcdzsd>`_.

The analyzers and respective code fix providers are grouped under the same folder. Not all analyzers have a code fix provider.

All the tests use either the `CSharpDiagnosticAnalyzerTest <https://github.com/edumserrano/roslyn-analyzers/blob/master/Tests/Analyzers.Tests/_TestEnvironment/Base/CSharpDiagnosticAnalyzerTest.cs>`_ or the `CSharpCodeFixProviderTest <https://github.com/edumserrano/roslyn-analyzers/blob/master/Tests/Analyzers.Tests/_TestEnvironment/Base/CSharpCodeFixProviderTest.cs>`_ as the base class depending if it's a test for an analyzer or for a code fix provider.

The classes inside `Tests/Analyzers.Tests/_TestEnvironment/Roslyn <https://github.com/edumserrano/roslyn-analyzers/tree/master/Tests/Analyzers.Tests/_TestEnvironment/Roslyn>`_ are a :ref:`refactored version of the classes provided by the default template <how-to-test-an-analyzer>` for unit testing analyzers and code fix providers. 

To debug the analyzers set the project DebugAnalyzers.Vsix as the startup project and follow the instructions :ref:`here <how-to-debug>`.

.. note:: Because I used the Tuple syntax from C# 7.0 on the analyzers project I had to do the following for the unit test project to work:

   * Update the NuGet package System.ValueTuple to 4.3.1 on the unit test project.
   * Install the Nuget package System.Composition 1.0.31 on the unit test project.

RoslynAnalyzersTestData.sln
---------------------------

Contains test cases to be used in the unit test project. 

It is also used when debugging the analyzers because once you run the DebugAnalyzers.Vsix you can chose to open this solution with the experimental version of Visual Studio and then open the classes that should or should not trigger the analyzer. While doing this you can set breakpoints and debug the behavior of the analyzer/code fix provider.


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