Railway-oriented programming
============================

You can easily extend the monads to provide useful operations that allow you to code in a different way.

Consider the following example::

	public Result DoSomething()
	{
		Result saveResult = /*save something to the db*/;
		if(!saveResult.IsSuccess)
		{
		return saveResult;
		}

		Result sendEmailResult = /*send an email*/;
		if(!sendEmailResult.IsSuccess)
		{
		return sendEmailResult;
		}

		Result createAuditEntry = /*do some auditing*/;
		return createAuditEntry;
	}

With extensions methods you can 