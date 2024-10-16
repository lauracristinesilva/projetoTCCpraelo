Create Database bd_infinity; -- creation the database of Infinity Sky enterprise
USE bd_infinity; -- using database --

select * from Cliente_tbl;
select * from Telefone_tbl;

-- tables --
Create table Pais_tbl(
	id_pais int primary key auto_increment,
    nome_pais varchar(50) not null,
    clima_pais varchar(50) not null,
    comidas_tip varchar(50) not null,
    moeda_pais varchar(20) not null,
    descricao_pais varchar(800) not null
);

Create table Plano_tbl(
	id_plano int primary key auto_increment,
    hospedagem_plano varchar(100) not null,
    curso_plano varchar(100) not null,
    instituicao_plano varchar(150) not null,
    periodo_plano varchar(20) not null,
    descricao_plano varchar(800) not null,
    imagem_plano VARCHAR(255),
    id_pais int not null,
    valor decimal(8,2) NOT NULL,
    constraint Fk_id_pais foreign key (id_pais) references Pais_tbl(id_pais)
);

create table Telefone_tbl(
	id_telefone int primary key auto_increment,
	telefone_cliente varchar(11) not null
);

/*
create table Cliente_tbl(
	id_cliente int primary key auto_increment,
    nome_cliente varchar(100) not null,
    email_cliente varchar(150) not null,
    senha_cliente varchar(10) not null,
    cpf_cliente varchar(11) not null,
    id_telefone int not null,
    data_nascimento date not null,
    constraint FK_id_telefone_cliente foreign key (id_telefone) references Telefone_tbl(id_telefone),
    constraint UC_cpf_cliente unique (cpf_cliente)
);

*/

create table Cliente_tbl(
	id_cliente int primary key auto_increment,
    nome_cliente varchar(100) not null,
    email_cliente varchar(150) not null,
    senha_cliente varchar(10) not null,
    cpf_cliente varchar(11) not null,
    telefone varchar(20) not null,
    data_nascimento varchar(20) not null
);



select * from Cliente_tbl;

create table Carrinho_tbl(
	id_carrinho int primary key auto_increment,
    itens_carrinho int not null,
    valor_total_carrinho decimal(8,2) not null,
    id_plano int not null,
    id_usuario int not null,
    constraint FK_id_plano_carrinho foreign key (id_plano) references Plano_tbl(id_plano),
    constraint FK_id_usuario_carrinho foreign key (id_usuario) references Usuario_tbl(id_usuario)
);

create table Favorito_tbl(
	id_favorito int primary key auto_increment,
    status_favorito varchar(10) not null,
    id_plano int not null,
    id_usuario int not null,
    constraint FK_id_plano_favorito foreign key (id_plano) references Plano_tbl(id_plano),
    constraint FK_id_usuario_favorito foreign key (id_usuario) references Usuario_tbl(id_usuario)
);

create table Pagamento_tbl(
	id_pagamento int primary key auto_increment,
    forma_pagamento varchar(10) not null,
    status_pagamento varchar(10) not null,
    hora_pagamento time not null,
    valor_pagamento decimal(8,2) not null,
    id_carrinho int not null,
    constraint FK_id_carrinho_pag foreign key (id_carrinho) references Carrinho_tbl(id_carrinho)
);

create table NF_tbl(
	id_nf int primary key auto_increment,
    valor_nf decimal (8,2),
    data_emissao date not null,
    hora_emissao time not null,
    id_pagamento int not null,
    constraint FK_id_pagamento_nf foreign key (id_pagamento) references Pagamento_tbl(id_pagamento)
);

						-- Inserts on the tables --

insert into Pais_tbl()
	values (default, 'Itália', 'Mediterrâneo', 'Pizza; Lasanha; Spaghetti', 'Euro', 'A Itália é um país localizado no sul da Europa, conhecido por seu formato característico de bota. É famoso por sua rica história, cultura e arte, com cidades icônicas como Roma, Florença e Veneza. A Itália é também célebre por sua culinária, incluindo pratos como pizza e pasta, e por suas paisagens variadas, que vão desde as praias do Mediterrâneo até as montanhas dos Alpes. É um destino popular para turismo, oferecendo uma mistura de patrimônio histórico, belezas naturais e um estilo de vida vibrante.'),
		   (default, 'Espanha', 'Mediterrâneo', 'Paella; Cocido Madrileño; Gazpacho', 'Euro', 'A Espanha é um país no sul da Europa, conhecido por suas praias, montanhas e cidades vibrantes como Madrid e Barcelona. Tem um clima variado, com verões quentes e invernos suaves em muitas áreas. É famosa pela sua comida, como paella e tapas, e por festas tradicionais animadas.'),
           (default, 'Austrália', 'Tropical', 'Pies (Tortas); Molho barbecue; Carneiro assado', 'Dólar australiano', 'A Austrália é um país e continente localizado no Hemisfério Sul. Conhecida por suas paisagens diversificadas, que vão de praias e desertos a florestas tropicais, a Austrália é famosa por sua vida selvagem única, como cangurus e coalas. Suas cidades principais são Sydney, Melbourne e Brisbane. É um destino popular para turismo, com um estilo de vida descontraído e uma rica herança cultural.');


INSERT INTO Plano_tbl (hospedagem_plano, curso_plano, instituicao_plano, periodo_plano, descricao_plano, imagem_plano, id_pais, valor)
VALUES ('Acomodação em Residência Estudantil (Dormitório)', 'Italiano', 'Università di Bologna', '8 meses', 'O plano inclui 8 meses de estudo intensivo de italiano na Università di Bologna...', 'img_planos/planoafricadosul1.png', 1, 12000.00),
       ('Hospedagem Temporária (Airbnb, etc.)', 'Espanhol', 'Universidade Autônoma de Barcelona', '4 meses', 'Este plano oferece 4 meses de curso de espanhol na Universidade Autônoma de Barcelona...', 'img_planos/planoargentina1.png', 2, 15000.00);   
insert into Telefone_tbl ()
	values (default, '11988430419'),
		   (default, '13997525728');
           
insert into Cliente_tbl()
	values (default, 'Rogério Marques Figueredo', 'rogerio.figueredo@gmail.com', 'rog2345', '24498437527', 1, '1975-04-12'),
		   (default, 'Paula Koga Muchão', 'paula.muchao@gmail.com', 'paul10', '96969004490', 2, '2002-02-12');
           
insert into Carrinho_tbl()
	values (default, 1, 33000.00, 1, 1),
		   (default, 1, 65000.00, 2, 2);
           
insert into Favorito_tbl()
	values (default, 'salvo', 2, 1),
		   (default, 'excluido', 1, 2);
           
insert into Pagamento_tbl()
	values (default, 'Pix', 'realizado', '17:52:06', 1245.90, 1),
		   (default, 'Boleto', 'realizado', '08:05:51', 2545.90, 1);

insert into NF_tbl()
	values (default, 33000.00, '2024-10-20', '14:32:14',1),
		   (default, 56457.90, '2024-01-03', '16:52:00',2);


-- Procedures on the tables --

						/* consultar telefone do User */
Delimiter $$
create procedure spSelectTelefone(vId_telefone int)
begin
	select * from Cliente_tbl where id_telefone = vId_telefone;
end $$

call spselectTelefone(2);

						/* Primeiro será consultar carrinho do User */

Delimiter $$
create procedure spSelectCarrinho(vId_usuario int)
begin
	select * from Carrinho_tbl where id_usuario = vId_usuario;
end $$

call spselectcarrinho(1);
call spselectcarrinho(2);

						/* Segundo consultar Favorito do User */

Delimiter $$
create procedure spSelectFavorito(vId_usuario int)
begin
	select * from Favorito_tbl where id_usuario = vId_usuario;
end $$

call spselectfavorito(2);

						/* 2º realizar insert no favorito do User */

Delimiter $$
create procedure spInsertFavorito(vStatus_favorito varchar(10), vId_plano int, vId_usuario int)
begin
	insert into Favorito_tbl(status_favorito, id_plano, id_usuario)
		values(vStatus_favorito, vId_plano, vId_usuario);
end $$

call spinsertFavorito('salvo', 2, 2);

						/* 1º realizar insert no carrinho do User */

Delimiter $$
create procedure spInsertCarrinho(vItens_carrinho int, vValor_total_carrinho decimal(8,2), vId_plano int, vId_usuario int)
begin
	insert into Carrinho_tbl(itens_carrinho, valor_total_carrinho, id_plano, id_usuario)
		values(vItens_carrinho, vValor_total_carrinho, vId_plano, vId_usuario);
end $$

call spinsertCarrinho();

						/* 4º realizar insert no pagamento do User */

Delimiter $$
create procedure spInsertPagamento(vForma_pagamento varchar(10), vStatus_pagamento varchar(10), vHora_pagamento time, vValor_pagamento decimal(8,2), vId_carrinho int)
begin
	if exists (select * from Carrinho_tbl where id_carrinho = vId_carrinho) then
		insert into Pagamento_tbl(forma_pagamento, status_pagamento, hora_pagamento, valor_pagamento, id_carrinho)
			values(vForma_pagamento, vStatus_pagamentos, vHora_pagamento, vValor_pagamento, vId_carrinho);
	end if;
end $$

call spinsertPagamento('Pix', 'pendente', '12:00:00', 350.00, 3);
call spinsertPagamento('Pix', 'realizado', '12:00:00', 350.00, 2);

						/* 5º realizar insert nf do User */

Delimiter $$
create procedure spInsertNF(vValor_nf decimal(8,2), vData_emissao date, vHora_emissao time, vId_pagamento int)
begin
	if exists (select * from Pagamento_tbl where id_pagamento = vId_pagamento) then
		insert into NF_tbl(valor_nf, data_emissao, hora_emissao, id_pagamento)
			values(vValor_nf, vData_emissao, vHora_emissao, vId_pagamento);
	end if;
end $$

call spinsertNF();



						-- Triggers on the tables --
						/*  */

						-- Joins on the tables --

						-- describes --
                        
describe Pais_tbl;
describe Plano_tbl;
describe Usuario_tbl;
describe Carrinho_tbl;
describe Favorito_tbl;
describe Pagamento_tbl;
describe Telefone_tbl;
describe NF_tbl;

						-- Selects for the tables --
                        
select * from Pais_tbl;
select * from Plano_tbl;
select * from Usuario_tbl;
select * from Carrinho_tbl;
select * from Favorito_tbl;
select * from Pagamento_tbl;
select * from Telefone_tbl;
select * from NF_tbl;
select * from Pais_tbl where id_pais = 2;

						-- alter table (não recomendável) rename --
                        
alter table Pais_tbl modify column descricao_pais varchar(800);
alter table Pais_tbl modify column moeda_pais varchar(20) not null;
alter table Plano_tbl modify column descricao_plano varchar(800) not null;

						-- shows --
                        
show databases;
show tables;
show variables like "sql_safe_updates";

						-- Funções de tempo, suer e data --
                        
select current_date();
select current_time();
select current_user();
set sql_safe_updates = 0;

						-- drops --
                        
drop database bd_infinity;
drop table Pais_tbl;
drop table Plano_tbl;
drop table Usuario_tbl;
drop table Carrinho_tbl;
drop table Favorito_tbl;
drop table Pagamento_tbl;
drop table Telefone_tbl;
drop table NF_tbl;
drop procedure spselectTelefone;
drop procedure spinsertPagamento;
drop procedure spUpdateCarrinho;



-- View --

