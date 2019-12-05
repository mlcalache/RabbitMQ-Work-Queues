# RabbitMQ-Work-Queues
RabbitMQ-Work-Queues

## English

Example of solution using the tutorial
https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html

### Database configuration

First, notice that the service (worker) from this solution aims on insert a record in the SQL Server database.

The used table is defined as api.Log_Portal_Generic.
And there is an entity in the Domain project for this table (mapping).

So, if you want to run this solution in your local machine, it's required to either create this object (api.Log_Portal_Generic) in your SQL Server database or change the solution to use an existing object from your database.

Don't forget to change the connection string in the app.config file from your Worker (RabbitMQ-Work-Queues.Server) and in the Entity Framework project, in case you want to use the Wizard connection tool or run a Migrations.

### Usage

To facilitate the usage of this solution, I have created 2 powershell scripts:

QueueClient.ps1 to insert into the queue.
QueueServer.ps1 to create workers to process the queue.

QueueClient.ps1 must receive a number as a parameter. This number defines the message quantity to be inserted into the queue.
For example, "QueueClient.ps1 1000", "QueueClient.ps1 10", or "QueueClient.ps1 1000000".

QueueServer.ps1 must receive a number as a parameter. This number defines the workers quantity to be created to process the queue.
For example, "QueueServer.ps1 2", "QueueServer.ps1 10", or "QueueServer.ps1 100".

There is no right order to create the messages or to create the workers.
Additionally, while they are already running, you are able to create more messages and more workers if you want.

## Português

### Configuração do Banco de Dados

Primeiro, note que o serviço (worker) desta solução tem o propósito de fazer uma inserção no banco de dados SQL Server.

A tabela utilizada está definida como api.Log_Portal_Generic.
E existe a entidade no projeto Domain.

Portanto, caso queira rodar na sua máquina local, faz-se necessário ou criar esse objeto (api.Log_Portal_Generic) no seu banco SQL server ou então alterar a solução para um objeto já existe no seu banco de dados).

Não se esqueça de mudar a connection string no app.config do Worker (RabbitMQ-Work-Queues.Server) e do Entity Framework (caso queira abrir o Wizard de Conexão ou então rodar o Migrations).

### Utilização

Para facilitar o uso da solução, eu criei dois scripts powershell:

QueueClient.ps1 para inserir na fila.
QueueServer.ps1 para criar workers para processar a fila.

QueueClient.ps1 deve receber como parâmetro a quantidade de mensagens a serem inseridas na fila.
Por exemplo, "QueueClient.ps1 1000", "QueueClient.ps1 10", ou "QueueClient.ps1 1000000".

QueueServer.ps1 deve receber como parâmetro a quantidade de workers que serão criados para processar a fila.
Por exemplo, "QueueServer.ps1 2", "QueueServer.ps1 10", ou "QueueServer.ps1 100".

Não há ordem necessária para criar as mensagens ou criar os workers.
Inclusive, enquanto já estão executando, você pode criar mais mensagens e mais workers.
