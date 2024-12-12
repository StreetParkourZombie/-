using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TaiSuu
{
    class Program
    {
        // Check balance and bet
        static bool BetandCheck(double userBalance,ref  double betAmount)
        {
            if (userBalance >= 500000) // Check if the user has enough balance
            {
                // Input bet amount
                Console.Write("- Nhập số tiền muốn cược (phải từ 500K đồng trở lên): ");
                double.TryParse(Console.ReadLine(), out betAmount);

                while (true) // Check whether the bet amount inputed is approriate or not
                {
                    if (betAmount < 500000)
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
                    else break;
                }
            }
            else // If the user don't have enough balance, return false
            {
                Console.WriteLine("... Số dư của bạn không đủ để tiếp tục. Số dư cần ít nhất 500K ...\n\n\n");
                return false;
            }
            return true;
        }


        // Check result without draw result
        static void annouceResult(bool result, ref double userBalance, ref double betAmount)
        {
            if (result) // The case the user wins
            {
                userBalance += betAmount;
                betAmount = 0; // Reset bet amount for a new bet
                Console.WriteLine("====! Bạn đã thắng !====");
            }
            else // The case the user loses
            {
                userBalance -= betAmount;
                betAmount = 0; // Reset bet amount for a new bet
                Console.WriteLine(".... Bạn thua ....");
            }
            Console.WriteLine(" ==>  Số dư của bạn: {0}  <===",userBalance); // Annouce user's balance
        }

        // Check result within draw result
        static void annouceResult(bool result, ref double userBalance, ref double betAmount, bool draw)
        {
            if (!draw)
            {
                if (result) // The case the user wins
                {
                    userBalance += betAmount;
                    betAmount = 0; // Reset bet amount for a new bet
                    Console.WriteLine("====! Bạn đã thắng !====");
                }
                else // The case the user loses
                {
                    userBalance -= betAmount;
                    betAmount = 0; // Reset bet amount for a new bet
                    Console.WriteLine(".... Bạn thua ....");
                }
            }
            else Console.WriteLine("~~~~ Kết quả hoà ! ~~~~");

            Console.WriteLine(" ==>  Số dư của bạn: {0}  <===", userBalance); // Annouce user's balance
        }

        //// Over/Under Bet
        static bool OverUnderBet(int betNumber, ref bool draw)
        {
            Random dice = new Random();

            int res1 = dice.Next(1, 7);
            int res2 = dice.Next(1, 7);
            int res3 = dice.Next(1, 7);

            Console.WriteLine("Xúc xắc 1: {0}", res1);
            Console.WriteLine("Xúc xắc 2: {0}", res2);
            Console.WriteLine("Xúc xắc 3: {0}", res3);

            int sum = res1 + res2 + res3;

            // Return the bet result: UNDER is 0; OVER is 1; draw is others
            int finalRes = (sum >= 4 && sum <= 10) ? 0 : (sum >= 11 && sum <= 17) ? 1 : -1;

            // If the result is draw, the user is still counted as a winner
            if (finalRes == -1)
            {
                draw = true;
                return true;
            }
            return finalRes == betNumber;
        }

        //// Odd/Even Bet
        static bool OddEvenBet(int betNumber)
        {
            Random dice = new Random();

            int res1 = dice.Next(1, 7);
            int res2 = dice.Next(1, 7);
            int res3 = dice.Next(1, 7);

            int sum = res1 + res2 + res3;

            bool isEven = sum % 2 == 0;
            bool isBetEven = betNumber % 2 == 0;

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
                                  "1. Cược tài/xỉu\n" +
                                  "2. Cược chẵn/lẻ\n" +
                                  "3. Kiểm tra số dư\n" +
                                  "4. Thoát"




                             );

                Console.Write("- Vui lòng nhập lựa chọn: ");
                int.TryParse(Console.ReadLine(), out c);


                switch (c)
                {
                    case 1:
                        Console.WriteLine("\n==> Bạn đã chọn cược tài/xỉu <==");
                        enoughBalance = BetandCheck(userBalance, ref betAmount);

                        if (!enoughBalance)
                        {
                            break;
                        }

                        Console.Write("    -> Hãy chọn nhập lựa chọn (0.Xỉu / 1.Tài): ");
                        int betNumber;
                        int.TryParse(Console.ReadLine(), out betNumber);

                        while (betNumber != 0 && betNumber != 1)
                        {
                            Console.Write("    ... Cú pháp không đúng! Vui lòng nhập lại (0.Chẵn / 1.Lẻ): ");
                            int.TryParse(Console.ReadLine(), out betNumber);
                        }

                        if (betNumber == 0)
                        {
                            Console.WriteLine("  => Bạn đã chọn XỈU");
                        }
                        else
                        {
                            Console.WriteLine("  => Bạn đã chọn TÀI");
                        }

                        bool draw = false; // Assign draw result is false as it is unknown  
                        bool result = OverUnderBet(betNumber,ref draw);


                        annouceResult(result, ref userBalance, ref betAmount, draw);

                        Console.WriteLine("\n\n");


                        break;

                    case 2:
                        Console.WriteLine("\n==> Bạn đã chọn cược chẵn/lẻ <==");
                        enoughBalance = BetandCheck(userBalance, ref betAmount);

                        if (!enoughBalance)
                        {
                            break;
                        }

                        Console.Write("    -> Hãy chọn một số (0.Chẵn / 1.Lẻ): ");
                        
                        int.TryParse(Console.ReadLine(), out betNumber);
                        
                        while (betNumber != 0 && betNumber != 1)
                        {
                            Console.Write("    ... Cú pháp không đúng! Vui lòng nhập lại (0.Chẵn / 1.Lẻ): ");
                            int.TryParse(Console.ReadLine(), out betNumber);
                        }

                        if (betNumber == 0)
                        {
                            Console.WriteLine("  => Bạn đã chọn CHẴN");
                        }
                        else
                        {
                            Console.WriteLine("  => Bạn đã chọn LẺ");
                        }
                        result = OddEvenBet(betNumber);

                        annouceResult(result, ref userBalance, ref betAmount);

                        Console.WriteLine("\n\n");
                        break;

                    case 3:
                        Console.WriteLine(" ==>  Số dư của bạn: {0}  <===\n\n\n", userBalance);
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("==============>>> Hẹn gặp lại! <<<==============");
                        Environment.Exit(1);
                        break;

                    default:
                        Console.WriteLine("... Lựu chọn không hợp lệ ...\n\n\n");
                        break;
                }

            } while (c!=4);

        }


        /// Choose a difficulty
        static void difficultyMenu(ref double userBalance)
        {
            int c;
            
            Console.WriteLine("==> CHỌN ĐỘ KHÓ\n" +
                              "1. Cực dễ: 20000000\n" +
                              "2. Dễ: 10000000\n" +
                              "3. Thường: 5000000\n" +
                              "4. Khó: 1500000\n" +
                              "5. Cò quay Nga: 500000"

                             );

            Console.Write("- Vui lòng nhập lựa chọn: ");
            int.TryParse(Console.ReadLine(), out c);

            while (c < 1 || c > 5)
            {
                Console.Write("\n... Lựu chọn không hợp lệ! Vui lòng nhập lại: ");
                int.TryParse(Console.ReadLine(), out c);
            }

            switch (c)
            {
                case 1:
                    userBalance = 20000000;
                    Console.WriteLine("Bạn đã chọn mức độ CỰC DỄ. Chúc vui vẻ!\n\n\n");
                    break;
                case 2:
                    userBalance = 10000000;
                    Console.WriteLine("Bạn đã chọn mức độ DỄ. Chúc vui vẻ!\n\n\n");
                    break;
                case 3:
                    userBalance = 5000000;
                    Console.WriteLine("Bạn đã chọn mức độ THƯỜNG. Chúc vui vẻ!\n\n\n");
                    break;
                case 4:
                    userBalance = 1500000;
                    Console.WriteLine("Bạn đã chọn mức độ KHÓ. Chúc vui vẻ!\n\n\n");
                    break;
                case 5:
                    userBalance = 500000;
                    Console.WriteLine("Bạn đã chọn mức độ CÒ QUAY NGA. Chúc tan nhà nát cửa!!!\n\n\n");
                    break;
            }
            
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            double userBalance = 0;
            difficultyMenu(ref userBalance);

            Menu(ref userBalance);
        }
    }
}
