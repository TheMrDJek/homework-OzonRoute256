# Домашняя работа по курсу "Продвинутая разработка микросервисов на C#" от Ozon Route 256 
## Technologies implemented: 
- ASP.NET 6.0
- Dapper
- Apache Kafka
- MediatR
## Список задач
<details>
   <summary><b>Улучшение производительности</b></summary>

**Задание**

Ускорить метод Process и показать ускорение с помощью бенчмарка

**Условия**
 - Время на выполнение 3 часа
 - Бизнес смысл:
    - Получает список товаров(ItemDto)
    - Группирует по размеру используя метод GetSizeType
    - Сохраняет сгруппированные товары в SaveBySize
    - Определяет какие товары подходят для главной странице
    - Сохраняет выбранные товары
 - Ограничения:
    - нельзя менять бизнесовый смысл методов/объектов, т.е. методы ConvertItems, 
GetSizeType, SaveBySize, IsForMainPage, SendItemsForMainPage - должны остаться и бизнесово выполнять свою задачу(их интерфейс менять допускается) 
    - Так же допускает менять структуру классов, сохраняя их ответственность,
 запрещается менять _dataFromExternalService и объект ItemDto

**Результаты**

   ![До](/assets/homework1Issue.png)
   
   ![После](/assets/homework1IssueResult.png)

**Ссылки**

[homework-1](/homework-1)


</details>

<details>
   <summary><b>Grps-сервисы</b></summary>

**Задание**

Необходимо разработать два сервиса.

**Условия**
- Время на выполнение: 20 часов
1. Сервис-эмулятор погодных датчиков.
   - Сервис должен генерировать события от минимум двух погодных датчиков (можно больше) уличного и датчика внутри помещения
	- В событии от датчиков должны быть данные по текущей температуре, влажности и содержанию CO2. Желательно учесть, что уровень CO2 на уличном датчике обычно +- одинаковый, в отличии от датчиков, находящихся в помещениях.
	 - Должен реализовывать Grpc-сервис, который в потоковом режиме будет возвращать события из датчиков.
	 - Должен реализовывать REST-метод, который возвращает текущие параметры каждого датчика. Например, сейчас нужно прямо узнать какие последние данные были на датчике.
	- Интервал генерации событий вынести в настройки.
	- Интервал не должен превышать 2 секунд. Т.е. события должны генерироваться хотя бы одно в 2 секунды по каждому датчику.
	- Не стоит выставлять и слишком маленький интервал - минимальное значение 100мс.


2. Сервис-клиент обработки событий от сервиса-эмулятора погодных датчиков
   - Должен уметь подписывается на получение данных от конкретного датчика или группы датчиков.
   - Должен уметь отписываться от получения информации по одному или всем датчикам.
   - Должен взаимодействовать с сервисом-эмулятором через полнодуплексный grpc stream.
   - Должен уметь переподнимать поток, если вдруг происходит разрыв связи. Например, если сервис-эмулятор остановлен, то необходимо пробовать подключаться с нему, до победного. Плюсом будет использование более сложного алгоритма ожидания, чем простой Delay.
	
- Должен оперативно аггрегировать информацию в следующих разрезах:
	1. Средняя температура по каждому датчику за 1 минуту.
	2. Средняя влажность по каждому датчику за 1 минуту.
	3. Максимальное и минимальное содержание CO2 по каждому датчику за 1 минуту.
		
- Должен иметь ручки, для чтения агрегированных данных в разрезе указанного интервала. Например, я хочу получить среднюю температуру за 10 минут начиная с 13:44. Значит ручка должна вытащить уже сагрегированные данные по 1 минуте, и посчитать из них среднюю за 10 минут.
- Должен иметь настройку для изменения интервала аггрегации. Если нужно аггрегировать не за 1 минуту, а за 10 минут.
- Должен иметь ручку для диагностики, которая выводит все сохраненные данные по каждому датчику.
	
- При разработке не использовать никаких внешних источников данных (БД,редис и т.п.).
- Все данные хранить в оперативной памяти. Данные должны быть доступны только на время работы сервиса.
- Допускается расширить функциональность сервисов по своему усмотрению, но минимальный набор требований должен быть выполнен обязательно.
- Критерии корректно выполнения задания:
	1. Все ручки реализованы
	2. Сервис-эмулятор генерирует события, а сервис-клиент эти события получает и обрабатывает
	3. На каждый запрос в ручку сервиса клиента - мы получаем корректно рассчитанный ответ.
	4. Код должен быть написан чисто, разделен на логические блоки. Желательно использовать стандартные настройки code style в райдере.	
	5. Клиент всегда может восстановить связь с сервисом-эмулятором.

**Ссылки**

[homework-2](/homework-2)
</details>

<details>
   <summary><b>Rate-Limiter</b></summary>

**Задание**

Необходимо реализовать rate-limiter (алгоритм fixed window) для web api в .net core приложении

**Настройки**
- количество одновременных запросов
- период времени
задаются глобально для всего приложения, при этом есть возможность:
   - переопределить параметры на методах в контроллере;
   - задать индивидуальные настройки для потребителей.
   - Клиенты идентифицируются по IP.
- В случае превышения лимита, клиент должен получить HTTP ошибку 429.
- В случае внешнего хранения не забыть про то, что внешние системы бывают недоступны и это не повод для сервиса работать совсем без каких-либо лимитов.

**Ссылки**

[homework-3](/homework-3)
</details>

<details>
   <summary><b>PostgreSQL & Dapper</b></summary>

**Задание**

Необходимо создать сервис с подключенной БД.

**Условия**
 - Вся работа с БД должна осуществляться через dapper
- Сервис должен уметь сохранять массивы заказов. Заказ состоит из следующих атрибутов
   - Id - guid
   - Идентификатор клиента(long)
   - Дата создания заказа (datetime)
   - Дата Выдачи заказа (datetime)
   - Статус заказа (New, InProgress, Pending)
   - Список товаров - (Массив из товаров(Id товара(long), Колличество(int)))
   - Id Склада отгрузки (long)
- Заказы нужно сохранять в партиционированную таблицу(партиционирование по складу отгрузки)
- Должен быть реализован метод умеющий искать заказы по складу отгрузки, статусу и диапазону дат
- Метод поиска должен возвращать результаты в виде потока данных(то есть нужно уметь читать данные частями через итератор, а не все сразу)
- Скрипт создания схемы бд должен быть просто в виде sql файла, не нужно делать механизм накатки миграций

**Ссылки**

[homework-4](/homework-4)
</details>

<details>
   <summary><b>Apache Kafka</b></summary>
   
**Задание**

   Реализовать батчевый kafka-консьюмер, который считывает N сообщений из топика и отдаёт их на обработку делегату обработчику void ProcessMessage<TKey, TValue>(IReadOnlyCollection<Message<TKey, TValue>> message) , который принимает на вход массив сообщений из топика.

- Важно предусмотреть:
   - Корректную обработку ошибок
   - Правильное сохранение оффсета в рамках партиции

- Задание со звёздочкой: 
   - реализовать exactly-once / идемпотентную обработку сообщений из топика с помощью редиса в качестве хранилища

**Ссылки**

[homework-5](/homework-5)
</details>

<details>
   <summary><b>Рефакторинг сервиса 1</b></summary>

**Проблема**

Вашему коллеге поставили задачу реализовать сервис поиска фильмов в котором вместе играют два актера.

У сервиса есть один метод API: 'POST /ActorsMatch', который на вход принимает Json:

```json
{
    "Actor1":"Keanu Reeves",             // Обязательный парамерт
    "Actor2": "Winona Ryder",            // Обязательный парамерт
    "MoviesOnly": true,                  // Необязательный параметр
}
```
Где
- Actor1 - первый актер
- Actor2 - второй актер
- MoviesOnly - вернуть только фильмы, то есть не озвучка и не телешоу

Пример ответа json:
```json
[
    "Destination Wedding",
    "The Private Lives of Pippa Lee",
    "A Scanner Darkly",
    "Bram Stoker's Dracula"
]
```
- Данные необходимо брать из https://imdb-api.com, для этого нужно там зарегистироваться и получить api_key. На бесплатном плане можно делать до 100 запросов в день.
 - Ваш коллега перед уходом в отпуск успел реализовать логику поиска совместных фильмов, а также закешировал запросы поиска ID актера по его имени в БД actorsdb, которая содержит всего одну таблиц actors

```sql
create table actors
(
  name     varchar(100) not null,
  actor_id varchar(100) not null
);
```
В этой таблице хранятся имя актера и его идентификатор.

 - Вам необходимо:
   - Отрефакторить код согласно принципам чистой архитектуры
   - Доработать недостающую функциональность
   - Сделать поиск с учетом фильтра MoviesOnly (для этого нужно выбирать только те фильмы, у которых Role это "Actress" или "Actor"

**Ссылки**

[homework-6](/homework-6)
</details>

<details>
   <summary><b>Рефакторинг сервиса 2 (Use Kafka)</b></summary>

**Проблема**
- Ваша кафка нестабильна, иногда запись в нее заканчивается ошибкой.
- Но вам необходиом обеспечить 100% запись ваших событий в Кафку.

**Решение**

Чтобы обеспечить транзакционность записи, необходимо реализовать на проекте паттерн [TransactionalOutbox](https://microservices.io/patterns/data/transactional-outbox.html)

Для этого:
* Создаете в БД отдельную таблицу payment_outbox
* В коде, который раньше слушал доменные события и отправлял сообщения в Кафку делаете запись в эту таблицу
* Создаете отдельный BackgroundWorker, который опрашивает эту таблицу на наличие новых сообщений (период опроса на ваше усмотрение, можно вынести в конфиг)
* Каждое сообщение он пытается записать в Кафку
* Если успех, то удаляет сообщение из таблицы
* Если неудача, то оставляет сообщение в таблице, но увеличивает значение количества попыток отправки
* Пытается делать отправку, пока количество попыток не превысит какой-то лимит (на ваше усмотрение)

**Ссылки**

[homework-7](/homework-7)
</details>