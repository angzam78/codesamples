/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

namespace SimpleCompiler
{
   using System;
   using System.Text.RegularExpressions;
   using System.Collections.Generic;
      
   public class Compiler
   {
        private List<string>.Enumerator parser;
		private List<string> args;
   
		public Ast pass1(string prog)
		{
			List<string> tokens=tokenize(prog);

			args = new List<string> ();
			parser = tokens.GetEnumerator();

			parser.MoveNext(); // token is "["
			while(parser.MoveNext() && parser.Current != "]") 
			{
				args.Add(parser.Current);
			} 
			parser.MoveNext(); // token is "]"

			return parseExpression(parseTerm(parseFactor()));
		}

		private Ast parseExpression(Ast term)
		{
			if (parser.Current == "+" || parser.Current == "-" )
			{   
				return parseExpression(new BinOp(getNextToken(), term , parseTerm(parseFactor())));
			}
			return term;
		}
			
		private Ast parseTerm(Ast factor)
		{
			if (parser.Current == "*" || parser.Current == "/" )
			{   
				return parseTerm(new BinOp(getNextToken(), factor, parseFactor()));
			}
			return factor;
		}

		private Ast parseFactor()
		{
			Ast factor;
			if (parser.Current == "(") {
				parser.MoveNext(); // token is "("
				factor = parseExpression(parseTerm(parseFactor()));
				parser.MoveNext(); // token is ")"
			} 
			else if (args.Contains(parser.Current)) 
				factor = new UnOp("arg", args.IndexOf(getNextToken()));
			else 
				factor = new UnOp("imm", int.Parse(getNextToken()));
			return factor;
		}

		private string getNextToken()
		{
			string token = parser.Current;
			parser.MoveNext();
			return token;
		}

		public Ast pass2(Ast ast)
		{
			string op = ast.op();

			if (op != "arg" && op != "imm") 
			{
				Ast lhs = pass2( ((BinOp)ast).a() );
				Ast rhs = pass2( ((BinOp)ast).b() );

				if (lhs.op() == "imm" && rhs.op() == "imm") 
				{
					UnOp a = (UnOp)lhs;
					UnOp b = (UnOp)rhs;

					if (op == "+")
						return new UnOp("imm", a.n() + b.n() );
					else if (op == "-")
						return new UnOp("imm", a.n() - b.n() );
					else if (op == "*")
						return new UnOp("imm", a.n() * b.n() );
					else if (op == "/")
						return new UnOp("imm", a.n() / b.n() );
				} 
				return new BinOp(op, lhs, rhs);
			} 
			return new UnOp(op, ((UnOp)ast).n() );
		}

		public List<string> pass3(Ast ast)
		{
			List<string> asm = new List<string>();
			Stack<Ast> stack = new Stack<Ast>();

			Ast node = ast;
			Ast lastNode = null;

			do 
            {
				if (node.op() != "imm" && node.op() != "arg")
				{
					stack.Push(node);
					node = ((BinOp)node).a();
				}
				else
				{
					Ast peekNode = stack.Peek();
					Ast peekNodeLeft = ((BinOp)peekNode).a();
					Ast peekNodeRight = ((BinOp)peekNode).b();

                    if(peekNodeRight != lastNode)
                    {
  					    if (peekNodeRight.op() != "imm" && peekNodeRight.op() != "arg")
  					    {
  						    node = peekNodeRight;
                            continue;
  					    }
  					    else
  					    {
  						    if (peekNodeLeft.op() == "imm")
  						    {
  							    asm.Add("IM " + ((UnOp)peekNodeLeft).n());
  							    asm.Add("PU");
  						    }
  						    else if (peekNodeLeft.op() == "arg")
  						    {	
  							    asm.Add("AR " + ((UnOp)peekNodeLeft).n());
  							    asm.Add("PU");
  						    }
  
  						    if (peekNodeRight.op() == "imm")
  							    asm.Add("IM " + ((UnOp)peekNodeRight).n());
  						    else // if (peekNodeRight.op() == "arg")
  							    asm.Add("AR " + ((UnOp)peekNodeRight).n());
                
                            asm.Add("PU");
  					    }
                    }
          
                    // remove push to avoid pop
                    if(asm[asm.Count-1] == "PU")
  				    	asm.RemoveAt(asm.Count-1);
  				    else
  				    	asm.Add("PO");
  
  				    asm.Add("SW");
  				    asm.Add("PO");
  
  				    if (peekNode.op() == "+") 
                        asm.Add("AD");
  				    else if (peekNode.op() == "-") 
                        asm.Add("SU");
  				    else if (peekNode.op() == "*") 
                        asm.Add("MU");
  				    else if (peekNode.op() == "/") 
                        asm.Add("DI");
  
  				    asm.Add("PU");
  
  				    lastNode = stack.Pop();
				}
			} while (stack.Count > 0);

			asm.RemoveAt(asm.Count-1); // remove last push to avoid pop
			
			return asm;
		}
     
        private List<string> tokenize(string input)
        {
            List<string> tokens = new List<string>();
            Regex rgxMain = new Regex("\\[|\\]|[-+*/=\\(\\)]|[A-Za-z_][A-Za-z0-9_]*|[0-9]*(\\.?[0-9]+)");
            MatchCollection matches = rgxMain.Matches(input);
            foreach (Match m in matches){
                tokens.Add(m.Groups[0].Value);
            }
            return tokens;
     }
   }
 }