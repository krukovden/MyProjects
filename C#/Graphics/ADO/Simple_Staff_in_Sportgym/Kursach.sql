create database Kursach_sport;

create table Users (id int Primary key  Identity(1,1),
					name varchar(100),
					number_card int ,
					birthday date,
					gender varchar(10),
					email varchar(20),
					tel varchar(20)
					);

create table Client(id int Identity(1,1) Primary key,
					id_abonement int not null,
					counts int null,
					limit_date date,
					id_user int not null,
					--constraint client_user Foreign key(id_user) references Users(id) on delete cascade
					);
					


create table Coach(id int Identity(1,1) Primary key,
					lave money,
					id_user int not null);
					--constraint coach_user Foreign key(id_user) references Users(id) on delete cascade);

Create table History_buy(id int Identity(1,1) Primary key,
							price money,
							id_abonement int not null,
							dates datetime,
							id_user int not null);

create table Abonement(id int Identity(1,1) Primary key,
						name varchar(50),
						limit_Enter time,
						limit_Exit time);

create table Visiting(id int Identity(1,1) Primary key,
						dateEnter datetime,
						deteExit datetime,
						id_user int not null
						);




insert into Users(name,number_card,birthday,gender,email,tel)	values('Павлов Вадим Петрович',1,'23-01-1968','Мужчина','email1@ukr.net','111-1111-111');
insert into Users(name,number_card,birthday,gender,email,tel)	values('Гайдай Иван Владимирович',2,'11-11-1980','Мужчина','email2@ukr.net','22-22222-222');
insert into Users(name,number_card,birthday,gender,email,tel)	values('Белова Снежанна Денисовна',3,'18-01-1981','Женщина','email3@ukr.net','333-3333-333');
insert into Users(name,number_card,birthday,gender,email,tel)	values('Труш Антон Дмитриевич',4,'11-12-1978','Мужчина','email4@ukr.net','444-4444-444');
insert into Users(name,number_card,birthday,gender,email,tel)	values('Белакур Валентина Богдановна',5,'08-07-1991','Женщина','email5@ukr.net','555-555-5555');


insert into Coach(lave,id_user) values(100,1);
insert into Coach(lave,id_user) values(80,2);


insert into Abonement (name, limit_Enter,limit_Exit) values ('Утрений', '08AM','12PM');
insert into Abonement (name, limit_Enter,limit_Exit) values ('Дневной','12:01PM','04PM');
insert into Abonement (name, limit_Enter,limit_Exit) values ('Вечерний','04:01PM','12AM');
insert into Abonement (name, limit_Enter,limit_Exit) values ('Безлимит',Null,Null);
insert into Abonement (name, limit_Enter,limit_Exit) values ('Разовый',Null,Null);
insert into Abonement (name, limit_Enter,limit_Exit) values ('Штраф',Null,Null);

insert into Client(id_abonement,counts,limit_date,id_user) values(1,null,'31-01-2014',3);
insert into Client(id_abonement,counts,limit_date,id_user) values(5,5,'31-01-2014',4);
insert into Client(id_abonement,counts,limit_date,id_user) values(3,null,'31-01-2014',5);


insert into History_buy(price,id_abonement,dates, id_user) values(2400,1,'31-12-2013',3);
insert into History_buy(price,id_abonement,dates,id_user) values(3400,3,'31-12-2013',5);
insert into History_buy(price,id_abonement,dates,id_user) values(100,5,'31-12-2013',3);

insert into Visiting(id_user,dateEnter,deteExit) values(1,'31-12-2013 01:02:03:04','31-12-2013 03:04:05:06');
insert into Visiting(id_user,dateEnter,deteExit) values(1,'06-02-2014 01:02:03:04','06-02-2014 03:04:05:06');

select Users.name as "Клиент",number_card,birthday,gender,email,tel,Abonement.name as "Название абонимента"
from Client join Users
on Client.id_user=Users.id
join Abonement
on Abonement.id=Client.id_abonement;


select * from Users	
select * from Visiting

select * from Coach

alter table Client drop constraint client_user;
alter table Coach drop constraint coach_user;
drop table Users;
drop table Client;
drop table  Coach;
drop table History_buy;
drop table Abonement;
drop table	Visiting;						  