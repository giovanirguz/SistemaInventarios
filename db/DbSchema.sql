
    if exists (select * from dbo.sysobjects where id = object_id(N'Clientes') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Clientes

    create table Clientes (
        Id INT IDENTITY NOT NULL,
       NoControl INT null,
       Nombre NVARCHAR(255) null,
       ApellidoPaterno NVARCHAR(255) null,
       ApellidoMaterno NVARCHAR(255) null,
       Rfc NVARCHAR(255) null,
       Direccion NVARCHAR(255) null,
       Telefono NVARCHAR(255) null,
       primary key (Id)
    )
