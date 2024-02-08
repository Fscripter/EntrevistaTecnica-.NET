use test1;

create table Jugador(
    id int primary key IDENTITY not null ,
    nombre varchar(35),
    apellidos varchar(35),
    fecha_registro DATETIME
);

create table Movimiento(
    id int primary key not null Identity,
    nombre varchar(35)
);

create table Partida(
    id int primary key IDENTITY not null,
    id_jugador_1 int not null Foreign key references Jugador(id),
    id_jugador_2 int not null  Foreign key references Jugador(id),
    ganador int Foreign key references Jugador(id)
);
create table Ronda(
    id int not null primary key IDENTITY,
    id_jugador_1 int not null Foreign key references Jugador(id),
    id_jugador_2 int not null  Foreign key references Jugador(id),
    ganador int,
    movimiento_jugador_1 int  Foreign key references Movimiento(id),
    movimiento_jugador_2 int  Foreign key references Movimiento(id),
    partida int Foreign key references Partida(id),
);


INSERT Into Movimiento (nombre) VALUES ('piedra')
INSERT Into Movimiento (nombre) VALUES ('papel')
INSERT Into Movimiento (nombre) VALUES ('tijeras')