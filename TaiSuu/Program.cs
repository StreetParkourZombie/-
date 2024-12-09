using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiSuu
{
    class Program
    {
        // Check balance and bet
        static bool BetandCheck(double userBalance,ref  double betAmount)
        {
            if (userBalance >= 500000)
            {
                Console.Write("- Nhập số tiền muốn cược (phải từ 500K đồng trở lên): ");
                double.TryParse(Console.ReadLine(), out betAmount);

                while (true)
                {
                    if(betAmount < 500000)
                    {
                        Console.Write("... Số tiền cược quá thấp! Vui lòng nhập số tiền cược từ 500K trở lên: ");
                        double.TryParse(Console.ReadLine(), out betAmount);

                        if (betAmount < 500000 && betAmount > userBalance)
                            break;
                    }
                    else if (betAmount > userBalance)
                    {
                        Console.Write("... Số tiền cược lớn hơn số dư! Vui lòng nhập lại: ");
                        double.TryParse(Console.ReadLine(), out betAmount);

                        if (betAmount >= 500000 && betAmount <= userBalance)
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("... Số dư của bạn không đủ để tiếp tục. Số dư cần ít nhất 500K ...\n\n\n");
                return false;
            }
            return true;
        }


        // Check result
        static void annouceResult(bool result, ref double userBalance, ref double betAmount)
        {
            if (result)
            {
                userBalance += betAmount;
                betAmount = 0; // Reset bet amount for a new bet
                Console.WriteLine("====! Bạn đã thắng !====");
            }
            else
            {
                userBalance -= betAmount;
                betAmount = 0; // Reset bet amount for a new bet
                Console.WriteLine(".... Bạn thua ....");
            }
            Console.WriteLine(" ==>  Số dư của bạn: {0}  <===",userBalance);
        }

        //// Odd/Even Bets
        static bool OddEvenBet(int bet)
        {
            Random dice = new Random();

            int res1 = dice.Next(1, 7);
            int res2 = dice.Next(1, 7);
            int res3 = dice.Next(1, 7);

            int sum = res1 + res2 + res3;

            bool isEven = sum % 2 == 0;
            bool isBetEven = bet % 2 == 0;

            Console.WriteLine("Xúc xắc 1: {0}", res1);
            Console.WriteLine("Xúc xắc 2: {0}", res2);
            Console.WriteLine("Xúc xắc 3: {0}", res3);

            return isEven == isBetEven;

        }

        // Menu
        static void Menu(ref double userBalance)
        {
            int c;
            double betAmount = 0;
            bool enoughBalance;
            do
            {
                Console.WriteLine("======================== MENU ========================\n" +
                              "1. Cược chẵn/lẻ\n" +
                              "2. Kiểm tra số dư\n" +
                              "3. Thoát"




                             );

                Console.Write("- Vui lòng nhập lựa chọn: ");
                int.TryParse(Console.ReadLine(), out c);


                switch (c)
                {
                    case 1:
                        Console.WriteLine("\n==> Bạn đã chọn cược chẵn/lẻ <==");
                        enoughBalance = BetandCheck(userBalance, ref betAmount);

                        if (!enoughBalance)
                        {
                            break;
                        }

                        Console.Write("    -> Hãy chọn một số (0.Chẵn / 1.Lẻ): ");
                        int betNumber;
                        int.TryParse(Console.ReadLine(), out betNumber);
                        
                        while (betNumber != 0 && betNumber != 1)
                        {
                            Console.Write("    ... Cú pháp không đúng! Vui lòng chọn một số (0.Chẵn / 1.Lẻ): ");
                            int.TryParse(Console.ReadLine(), out betNumber);
                        }

                        if (betNumber == 0)
                        {
                            betNumber = 2;
                            Console.WriteLine("  => Bạn đã chọn CHẴN");
                        }
                        else
                        {
                            betNumber = 1;
                            Console.WriteLine("  => Bạn đã chọn LẺ");
                        }
                        bool result = OddEvenBet(betNumber);

                        annouceResult(result, ref userBalance, ref betAmount);

                        Console.WriteLine("\n\n");
                        break;

                    case 2:
                        Console.WriteLine(" ==>  Số dư của bạn: {0}  <===\n\n\n", userBalance);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("==============>>> Hẹn gặp lại! <<<==============");
                        Environment.Exit(1);
                        break;
                    default:
                        Console.WriteLine("... Lựu chọn không hợp lệ ...");
                        break;
                }




            } while (c!=3);

        }


        static void Main(string[] args)
        {
            double userBalance = 5000000;

            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Menu(ref userBalance);
        }
    }
}
