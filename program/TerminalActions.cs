using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sharedEntities;
using dataPersistence;
 
namespace program
{ 
    public class TerminalActions
    {
        public static void CreateN(NationalTerminal pNterminal)
        {
            TerminalPersistence.CreateNterminal(pNterminal);
        }

        public static void UpdateN(NationalTerminal pNterminal)
        {
            TerminalPersistence.UpdateNterminal(pNterminal);
        }

        public static void DeleteN(NationalTerminal pNterminal)
        {
            TerminalPersistence.DeleteNterminal(pNterminal);
        }

        public static NationalTerminal ReadN(string pCodTerm)
        {
            return TerminalPersistence.findNterminal(pCodTerm);
        }

        public static List<NationalTerminal> ListNterminals()
        {
            return TerminalPersistence.ListNterminals();
        }

        public static void CreateI(InternationalTerminal pIterminal)
        {
            TerminalPersistence.CreateIterminal(pIterminal);
        }

        public static void UpdateI(InternationalTerminal pIterminal)
        {
            TerminalPersistence.UpdateIterminal(pIterminal);
        }

        public static void DeleteI(InternationalTerminal pIterminal)
        {
            TerminalPersistence.DeleteIterminal(pIterminal);
        }

        public static InternationalTerminal ReadI(string pCodTerm)
        {
            return TerminalPersistence.findIterminal(pCodTerm);
        }

        public static List<InternationalTerminal> ListIterminals()
        {
            return TerminalPersistence.ListIterminals();

        }

        public static void CreateI(NationalTerminal objNterminal)
        {
            throw new NotImplementedException();
        }
    }
}
