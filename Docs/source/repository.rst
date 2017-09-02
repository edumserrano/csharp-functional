.. include:: links.rst

.. _repository:

About the repository
====================

Source Code
-----------

There is only one solution in the |repo|_:
  
* `Source/CSharpFunctional/CSharpFunctional.sln <https://github.com/edumserrano/csharp-functional/tree/master/Source/CSharpFunctional>`_

Structure of the solution
~~~~~~~~~~~~~~~~~~~~~~~~~

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

Building the solution and running tests
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

This repository adheres to the `F5 manifesto <http://www.khalidabuhakmeh.com/the-f5-manifesto-for-net-developers>`_ so you should be able to clone, open the soluton in Visual Studio and build/run tests.

Documentation
-------------

The documentation for the repository can be found at the `Docs folder <https://github.com/edumserrano/csharp-functional/tree/master/Docs>`_. 

Read `this <https://docs.readthedocs.io/en/latest/getting_started.html#in-restructuredtext>`_ to understand how the documentation was created and how you can build it.

Building the docs
~~~~~~~~~~~~~~~~~

To build the docs you need:

* Python;
* Sphinx;
* Sphinx read the docs theme.

You can install Python from `here <https://www.python.org/downloads/windows/>`_ or if you have `chocolatey <https://chocolatey.org/>`_ you can do the following from PowerShell::

    choco install python

Sphinx is a tool that makes it easy to create beautiful documentation. Assuming you have Python already, install Sphinx by executing the following on PowerShell::

    pip install sphinx sphinx-autobuild

The read the docs theme is configured in the `conf.py <https://github.com/edumserrano/csharp-functional/blob/master/Docs/source/conf.py>`_ file. To get this theme execute the following on PowerShell::

    pip install sphinx_rtd_theme

For more information about the read the docs theme see `its repo <https://github.com/rtfd/sphinx_rtd_theme>`_.

Once you have setup your environment you can build the docs by running the `make.bat <https://github.com/edumserrano/csharp-functional/blob/master/Docs/make.bat>`_ file. You can also build the docs from Visual Studio Code as explained in the next section.

.. note:: These build instructions are focused for Windows users. If you are using a different OS then the instructions can't be taken word by word but the same requirements apply. Furthermore there is a `makefile <https://github.com/edumserrano/csharp-functional/blob/master/Docs/Makefile>`_ available for non Windows users.

Editing the docs
~~~~~~~~~~~~~~~~

Although you can show your editor of preference to work with `reStructuredText <http://www.sphinx-doc.org/en/stable/rest.html>`_, I have found `Visual Studio Code <https://code.visualstudio.com/>`_ to be very good for this task.

After installing Visual Studio Code I recommend installing the following extensions:

* `Code Spell Checker <https://marketplace.visualstudio.com/items?itemName=streetsidesoftware.code-spell-checker>`_;
* `reStructuredText <https://marketplace.visualstudio.com/items?itemName=lextudio.restructuredtext>`_;
* `Python <https://marketplace.visualstudio.com/items?itemName=donjayamanne.python>`_.

These will greatly increase your productivity while editing the documentation. For instance:

* The Code Spell Checker will highlight words it does not recognize and then you can use Ctrl+. on those words to correct them.
* The reStructuredText extension will 
* Visual Studio Code contains numerous features that will improve your productivity. Something as simple as providing auto-complete suggestions from the words already available on your documentation speeds up your typing a lot.

In the `.vscode folder <https://github.com/edumserrano/csharp-functional/tree/master/Docs/.vscode>`_ you have 3 configuration files:

* cSpell.json: contains configuration for the Code Spell Checker extension. You can add words to this file by using the Ctrl+. shortcut on words that the spell checker does not recognize and chose "Add to project dictionary".
* settings.json: contains the configuration for the reStructuredText extensions.
* tasks.json: contains the default build task definition used by Visual Studio Code when ask it to build. If you're using Visual Studio Code from a non Windows OS then you should change the command to execute the makefile instead of the make.bat.

Once you have installed Visual Studio code and the recommended extensions you can edit and build the docs by going to "File->Open Folder" and choosing the "/Docs/source" directory. If you do not open the source folder the build task will fail to run.