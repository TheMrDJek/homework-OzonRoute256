create database nano_payments;


create table payment
(
	id uuid not null,
	order_id varchar(100) not null,
	client_id varchar(100) not null,
	amount money not null,
	currency_code varchar(3) not null,
	status int4 not null,
	message varchar(100),
	provider_payment_id varchar(100),
	card_first_6 varchar(6),
	card_last_4 varchar(4),
	card_expiration_year int4,
	card_expiration_month int4
);

create unique index payment_id_uindex
	on payment (id);

create unique index payment_pk
	on payment (id);

alter table payment
	add constraint payment_pk
		primary key (id);
t
create table  payment_outbox
(
    id uiid not null,
    retry_count int not null
)