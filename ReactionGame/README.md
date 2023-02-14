## Inlämningsuppgift #1
### Fortsättningskurs C# Borås Yrkeshögskola 

Inlämning: Torsdag den 16:e februari via GitHub Classroom.
Individuell feedback och genomgång vid bestämda tider.
#### Vääldigt kort rapport skrivs i Rapport.md!

Läs instruktionerna noggrant! 

-------

## Uppgift: WebAPI och kommunikation

Utgångspunkten är ett litet spel som mäter reaktionstid. Nu vill vi se till så att spelet kan spara och ladda sina highscores online via ett web-api. 
Uppgiften går i huvudsak ut på att skapa WebAPIet som hanterar highscores, men även spelet måste modifieras för att använda sig av detta api.
APIet skall kunna ta emot data och spara ett nytt highscore.
Du ska också kunna hämta ut alla highscores från apiet, både så att spelet kan visa dom, men även så att det i förlängningen skulle kunna visas på tex en hemsida (inte en del av denna uppgift).

Apiet skall alltså användas av det medföljande reaktionsspelet. Det kommer vara nödvändigt att ändra i spelets kod för att lösa uppgiften. En highscore-klass finns att utgå från i reaktionstiddsspelet.

Flödet kan beskrivas så här:
1. När spelet startas ska alla highscores laddas ned via WebAPIet.
2. En spelare spelar reaktionspelet. 
3. Om spelaren får ett highscore får spelaren skriva in sitt namn.
4. Tiden och namnet skickas till WebAPI:et och lagras där på valfritt sätt.

## Funktionskrav

### **Krav för Godkänt**
* Både spel och WebApi skall gå att starta utan fel
* Spelet skall kunna hämta highscores via WebAPIet.
* Spelet skall kunna posta ett nytt highscore till WebAPIet.
* Spelet skall visa de 10 bästa highscores mellan varje försök.
* JSON-formatet skall användas för att skicka data mellan spelet och WebAPIet.
* De enpoints som skall finnas i ert API är definierade i denna tabell. Det är av yttersta vikt att ni följer denna specifikation till punkt och pricka!

| Metod | Endpoint | Beskrivning | Req. Body | Response body
| ------------ | ----------- | ---- | --------|---
| GET| /highscores | Hämta alla highscores | Ingen | Lista med highscores
| GET| /highscores/top10 | Hämta bara de 10 senaste highscores | Ingen | Lista med highscores
| POST| /highscores |Lägg till nytt highscore |Ett highscore| Highscoret som skapats


------

### **Ytterligare krav för Väl Godkänt**
* Använd LINQ där det är applicerbart
* Se till att hantera felaktig input till WebAPI
* Se till att returnera korrekt Response codes från dina endpoints
* Server: Utöver highscore med poäng och namn skall api:t också ta emot vilket spel det är som skickar highscoret, dvs på servern skall Poäng, Namn, Spelnamn (Klienten bestämmet det) samt även Tidpunkt lagras.
* Ytterligare endpoints som skall finnas för väl godkänt (de behöver inte vara implementerade i spelet i sig)
* Visa på god förståelse för response/request pipeline och HTTP-protokollet under individuell genomgång
* Kunna motivera val av struktur av Api:t

| Metod | Endpoint | Beskrivning | Req. Body | Response body
| ------------ | ----------- | ---- | --------|---
| GET| /highscores/{id} | Hämta highscore med ett visst ID |Ingen | Ett highscore
| GET| /highscores/{username} | Hämta alla highscores för ett visst användarnamn |Ingen | En lista med highscore
|DELETE|  /highscores|Ta bort alla highscores| Ingen| Ingen

------

### Valfria strechgoal som inte betygssätts:
* Gör en enkel hemsida som via wwwroot och static files visar en lista med alla highscores med hjälp av javascript. 
* Hur skulle du kunna göra för att skydda DELETE-endpointen så att inte vem som helst kan kalla på den?
