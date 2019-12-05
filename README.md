# RabbitMQ-Work-Queues
RabbitMQ-Work-Queues

Example of solution using the tutorial
https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html


# Configuração do Banco de Dados

Primeiro, note que o serviço (worker) desta solução tem o propósito de fazer uma inserção no banco de dados SQL Server.

A tabela utilida está definida como api.Log_Portal_Generic.
E existe a entidade no projeto Domain.

Portanto, caso queira rodar na sua máquina local, faz-se necessário ou criar esse objeto (api.Log_Portal_Generic) no seu banco SQL server ou então alterar a solução para um objeto já existe no seu banco de dados).

Não se esqueça de mudar a connection string no app.config do Worker (RabbitMQ-Work-Queues.Server) e do Entity Framework (caso queira abrir o Wizard de Conexão ou então rodar o Migrations).

# Utilização

Para facilitar o uso da solução, eu criei dois scripts powershell.

QueueClient.ps1 para inserir na fila
QueueServer.ps1 para criar workers para processar a fila

QueueClient.ps1 deve receber como parâmetro a quantidade de mensagens a serem inseridas na fila.
Por exemplo, "QueueClient.ps1 1000", "QueueClient.ps1 10", ou "QueueClient.ps1 1000000".

QueueServer.ps1 deve receber como parâmetro a quantidade de workers que serão criados para processar a fila.
Por exemplo, "QueueServer.ps1 2", "QueueServer.ps1 10", ou "QueueServer.ps1 100".

Não há ordem necessária para criar as mensagens ou criar os workers.
Inclusive, enquanto já estão executando, você pode criar mais mensagens e mais workers.
