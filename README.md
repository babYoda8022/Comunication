# Projeto de Comunicação com RabbitMQ

Esse projeto é apenas um estudo sobre o uso do `RabbitMQ` e `BackgroundService` 

O projeto é composto por duas partes principais:  
1. **Comunication**: Uma aplicação em `C#` baseada em `BackgroundService`, responsável por consumir e enviar mensagens.  
2. **RabbitMQ**: Uma biblioteca personalizada que fornece serviços de envio/consumo de mensagem através do `RabbitMQ.Client`

## Tecnologias Utilizadas
- **C#**: Linguagem principal.
- **BackgroundService**: Gerenciamento de tarefas em segundo plano.
- **RabbitMQ.Client**: Biblioteca usada para comunicação entre aplicações (mensageria).
- ?

## Funcionalidades

1. **Comunication**
- **ConsumerWorker** : Responsável pelo recebimento de mensagens em background
- **SendMessageWorker** Responsável pelo envio de mensagens em background

2. **RabbitMQ**
- **ConfigSettings** : Classe/Modelo usado para configuração das secrets do `RabbitMQ.Client`
- **SendAsync** : Responsável pelo envio de messagens através do `RabbitMQ.Client` de forma facilitada
- **ConsumerAsync** : Responsável pelo Consumo de messagens através do `RabbitMQ.Client` de forma facilitada

## Estrutura do Projeto
**Comunication**
```
Communication
├── Properties
│   └── launchSettings.json
├── Extensions
│   └── RabbitMQServices.cs
├── Worker
│   ├── ConsumerWorker.cs
│   └── SendMessageWorker.cs
├── appsettings.json
└── Program.cs
```

**RabbitMQ**
```
RabbitMQ
├── Iot
│   ├── ConfigSettings.cs
│   └── Ques.cs
├── Model
│   ├── ConsumerModel.cs
│   └── SendMessageModel.cs
└── Services
    ├── Interface
    │   └── IMessage.cs
    ├── Base.cs
    └── Message.cs
```
