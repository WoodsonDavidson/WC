using System;
using System.Collections.Generic;
using System.Text;

namespace WoodsonsCalculator2020
{
    class ClassForMethods
    {
        public ClassForMethods(string sourceStr)
        {
            srcStr = sourceStr;
        }
        static private Stack<string> stackForActs = new Stack<string>();
        static private Stack<string> outputStack = new Stack<string>();
        static private Stack<double> finalStack = new Stack<double>();
        static private string srcStr;
        static private Stack<string> srcStack = new Stack<string>();
        
        private static void unaryMinus ()
        {
            char[] charStr = srcStr.ToCharArray();
            for(int i = 0; i<srcStr.Length; i++)
            {
                if(charStr[i]=='-'&&(i==0||charStr[i-1]=='('))
                {
                    charStr[i] = 'm';
                }
            }
            srcStr = new string(charStr);
        }
        private static void Append(ref string str, char c)
        {
            str = str + c;
        }
        private static void StrToStack()
        {
            unaryMinus();
            string tempStr = "";
            foreach (char c in srcStr)
            {
                switch (c)
                {
                    case '+':
                        if (tempStr != "")
                        {
                            srcStack.Push(tempStr);
                            tempStr = "";
                        }
                        Append(ref tempStr, c);
                        srcStack.Push(tempStr);
                        tempStr = "";
                        break;
                    case '-':
                        if (tempStr != "")
                        {
                            srcStack.Push(tempStr);
                            tempStr = "";
                        }
                        Append(ref tempStr, c);
                        srcStack.Push(tempStr);
                        tempStr = "";
                        break;
                    case '*':
                        if (tempStr != "")
                        {
                            srcStack.Push(tempStr);
                            tempStr = "";
                        }
                        Append(ref tempStr, c);
                        srcStack.Push(tempStr);
                        tempStr = "";
                        break;
                    case '/':
                        if (tempStr != "")
                        {
                            srcStack.Push(tempStr);
                            tempStr = "";
                        }
                        Append(ref tempStr, c);
                        srcStack.Push(tempStr);
                        tempStr = "";
                        break;
                    case '^':
                        if (tempStr != "")
                        {
                            srcStack.Push(tempStr);
                            tempStr = "";
                        }
                        Append(ref tempStr, c);
                        srcStack.Push(tempStr);
                        tempStr = "";
                        break;
                    case '(':
                        if (tempStr != "")
                        {
                            srcStack.Push(tempStr);
                            tempStr = "";
                        }
                        Append(ref tempStr, c);
                        srcStack.Push(tempStr);
                        tempStr = "";
                        break;
                    case ')':
                        if (tempStr != "")
                        {
                            srcStack.Push(tempStr);
                            tempStr = "";
                        }
                        Append(ref tempStr, c);
                        srcStack.Push(tempStr);
                        tempStr = "";
                        break;
                    case 'm':
                        if (tempStr != "")
                        {
                            srcStack.Push(tempStr);
                            tempStr = "";
                        }
                        Append(ref tempStr, c);
                        srcStack.Push(tempStr);
                        tempStr = "";
                        break;
                    default:
                        Append(ref tempStr, c);
                        break;
                }
            }
            if (tempStr.Length != 0)
            {
                srcStack.Push(tempStr);
                tempStr = "";
            }
        }
        private static void ReverseStack(ref Stack<string> stack)
        {
            Stack<string> tempStack = new Stack<string>();
            StrToStack();
            while(stack.Count!=0)
            tempStack.Push(stack.Pop());
            stack = tempStack;
        }
        private static int DefPriority(in string str)
        {
            switch (str)
            {
                case "+":
                    return 2;
                case "-":
                    return 2;
                case "(":
                    return -1;
                case ")":
                    return -2;
                case "*":
                    return 3;
                case "/":
                    return 3;
                case "^":
                    return 4;
                case "m":
                    return 5;
                default:
                    return 0;
            }
        }
        private static void CheckPenultimPrior(in string str, in int curPrior)
        {
            if (stackForActs.Count != 0)
            {
                if (curPrior <= DefPriority(stackForActs.Peek()))
                {
                    outputStack.Push(stackForActs.Pop());
                    CheckPenultimPrior(in str, in curPrior);
                }
                else
                    stackForActs.Push(str);
            }
            else
                stackForActs.Push(str);
        }
        private static void RPN()
        {
            int curPrior;
            ReverseStack(ref srcStack);
            foreach(string str in srcStack)
            {
                curPrior = DefPriority(str);
                if (stackForActs.Count > 0||curPrior==0)
                {
                    if (curPrior == 0)
                    {//number
                        outputStack.Push(str);
                    }
                    else if (curPrior == -1)
                    {//opening paranthes
                        stackForActs.Push(str);
                    }
                    else if (curPrior == -2)
                    {//closing paranthes
                        while(stackForActs.Peek()!="(")
                        outputStack.Push(stackForActs.Pop());
                        stackForActs.Pop();
                    }
                    else if (curPrior == DefPriority(stackForActs.Peek()))
                    {//same priority
                        CheckPenultimPrior(in str, in curPrior);
                    }
                    else
                    {//current priority is smaller then penultimate
                        CheckPenultimPrior(in str, in curPrior);
                    }
                }
                else
                    stackForActs.Push(str);
            }
            foreach(string str in stackForActs)
            {
                outputStack.Push(str);
            }
            stackForActs.Clear();
        }
        public double Calculate()
        {
            double finalResult;
            double temp1, temp2, tempResult;
            RPN();
            ReverseStack(ref outputStack);
            Stack<string> tempOutputStack = new Stack<string>(outputStack);
            ReverseStack(ref tempOutputStack);
                foreach (string str in outputStack)
                {
                    switch (str)
                    {
                        case "+":
                            temp2 = Convert.ToDouble(finalStack.Pop());
                            temp1 = Convert.ToDouble(finalStack.Pop());
                            tempResult = temp1 + temp2;
                            finalStack.Push(tempResult);
                            tempOutputStack.Pop();
                            break;
                        case "-":
                            temp2 = Convert.ToDouble(finalStack.Pop());
                            temp1 = Convert.ToDouble(finalStack.Pop());
                            tempResult = temp1 - temp2;
                            finalStack.Push(tempResult);
                            tempOutputStack.Pop();
                            break;
                        case "*":
                            temp2 = Convert.ToDouble(finalStack.Pop());
                            temp1 = Convert.ToDouble(finalStack.Pop());
                            tempResult = temp1 * temp2;
                            finalStack.Push(tempResult);
                            tempOutputStack.Pop();
                            break;
                        case "/":
                            temp2 = Convert.ToDouble(finalStack.Pop());
                            temp1 = Convert.ToDouble(finalStack.Pop());
                            tempResult = temp1 / temp2;
                            finalStack.Push(tempResult);
                            tempOutputStack.Pop();
                            break;
                        case "^":
                            temp2 = Convert.ToDouble(finalStack.Pop());
                            temp1 = Convert.ToDouble(finalStack.Pop());
                            if (temp2 != 0)
                                tempResult = Math.Pow(temp1, temp2);
                            else
                                tempResult = 1;
                            finalStack.Push(tempResult);
                            tempOutputStack.Pop();
                            break;
                        case "m":
                            temp1 = Convert.ToDouble(finalStack.Pop());
                            tempResult = 0 - temp1;
                            finalStack.Push(tempResult);
                            tempOutputStack.Pop();
                            break;
                        default:
                            finalStack.Push(Convert.ToDouble(tempOutputStack.Pop()));
                            //finalStack.Push(Double.Parse(tempOutputStack.Pop()));
                            break;
                    }
                }
            finalResult = Convert.ToDouble(finalStack.Pop());
            outputStack.Clear();
            stackForActs.Clear();
            finalStack.Clear();
            srcStack.Clear();
            return finalResult;
        }
    }
}
