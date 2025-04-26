using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static  class MessageProvider
    {
        public static IEnumerable<string> GetQuotes() => 
                ["Jestem za, a nawet przeciw. – Lech Wałęsa",
                "Nie chcem, ale muszem. – Lech Wałęsa",
                "Balcerowicz musi odejść! – Andrzej Lepper",
                "Spieprzaj dziadu! – Jarosław Kaczyński",
                "Nicea albo śmierć! – Jan Maria Rokita",
                "Plusy dodatnie, plusy ujemne. – Lech Wałęsa",
                "Warto być przyzwoitym. – Władysław Bartoszewski",
                "Prawdziwego mężczyznę poznaje się nie po tym, jak zaczyna, ale jak kończy. – Leszek Miller",
                "Nie będzie Niemiec pluł nam w twarz. – Andrzej Duda",
                "Yes, yes, yes! – Kazimierz Marcinkiewicz"
                ];

        public static IEnumerable<string> GetTwitterAccounts() =>
                ["pisorgpl", "Platforma_org", "trzaskowski_x", "NawrockiKn", "SlawomirMentzen", "szymon_holownia", "ZandbergRAZEM", "MagdaBiejat", "GrzegorzBraun_"];
    }
}
