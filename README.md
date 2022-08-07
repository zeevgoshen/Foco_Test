# Foco_Test


<h2>- After cloning, run Update-Database in the Package Manager Console window to create the EF database. Mind the connection string.
(Pardon for stating the obvious)</h2>

<h2>- For testing the 2 endpoint I used the VS Code extension "Rest Client", the requests are in the Requests folder.</h2>
<h3>Swagger UI is disabled by choice since it's not as clear as the VS Code "Rest Client".</h3>
<h3>Run CreateTest.http to create a request and receive a ticket number (after installing the vs extension, press 'Send Request')
</h3>
<i>pressing several times with the same request (same person id and same test site) will not create several places in the queue
but will instead return the the original ticket number.</i>
<br>
<h3>The same ID number cannot have multi queues reserved, at several testing sites, this is intentional.</h3>


<h3>Run CallNextInLine.http to call the next queue ticket, in the order of addition/creation in the queue, and close the ticket.</h3>

<h3>Sending SMS is only a mock and not functional. If it was, the SMS message would have been written to a db table also with the receipient details</h3>

<b>Todos:</b> 

Better names, more error handling.
