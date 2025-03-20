Tisztelt Tanár Úr!

Elkészítettem a beadandót. 
A következő technológiákat használtam:
C# | .NET 4.8 | WPF | Visual Studio 2022

A program futtatásához kiraktam a főkönyvtárba az ".exe"-re mutató parancsikont

Egyszerű ACL kezelő kattintó felületes programról lenne szó.

Jelenleg a "read", "write" és "own" jogosultsági köröknek adtam valóságos erőforrást.

Bejelentkezés után változik az oldal, attól függően, hogy milyen joga van az adott személynek.
Jogosultságok:
- read -> El lehet olvasni a felhasználók listáját.
- write -> Létre lehet hozni új felhasználót, de csak red és write jogokkal.
- own -> Mindenhez jog, valamint extra jogosultságok felhasználóhoz rendelése (append, execute és own) lehetséges.

A 4 példa felhasználó neve és jelszava megegyezik
(name - pass)
admin - admin
readonly - readonly
writeonly - writeonly
readandwrite - readandwrite

A jelszavak egyébként titkosítva vannak tárolva a "users_permissions.txt" fájlban.

Remélem megfelel Tanár Úrnak! :)
