using UnityEngine;

/// <summary>
/// My very first version of a compiler for a language I'm making up as I go along.
/// Probably gonna look like a really bad C# clone.
/// </summary>
public static class Dumpiler
{
    /// <summary>
    /// Turn code into runnable shit
    /// </summary>
    /// <param name="code"></param>
    public static void Compile(string code) 
    {
        if (SplitIntoScopes(code, out Tree<string> scopes, out DumpilerError err))
        {
            Debug.Log($"Error occured at line {err.Line}: {err.Message}");
            return;
        }

        Debug.Log(scopes);
    }

    /// <summary>
    /// Creates a tree of scopes to then compile each scope and its subscopes properly
    /// </summary>
    /// <param name="code">The code to split into scopes</param>
    /// <param name="err">The error message if there was an error</param>
    /// <param name="mainScope">The scope containing all of the code</param>
    /// <returns>Whether the program HAD AN ERROR (not if it succeeded)</returns>
    private static bool SplitIntoScopes(string code, out Tree<string> mainScope, out DumpilerError err)
    {
        mainScope = new();
        err = null;


        Tree<string> curScope = mainScope; // start at main
        int sectionStart = -1;
        int line = 1;

        void AddSection(int charIndex)
        {
            string section = code.Substring(sectionStart, charIndex - sectionStart).Trim();
            if (!string.IsNullOrEmpty(section))
            {
                curScope.AddValue(section);
            }
        }

        for(int charIndex = 0; charIndex < code.Length; charIndex++)
        {
            if (code[charIndex] == '\n')
            {
                line++;
                continue;
            }

            if (code[charIndex] == '{')
            {
                if (sectionStart == -1)
                {
                    sectionStart = 0;
                }

                AddSection(charIndex);
                sectionStart = charIndex + 1;

                Tree<string> newScope = new();
                curScope.AddChild(newScope);
                curScope = newScope;
                continue;
            }

            if (code[charIndex] == '}')
            {
                if (sectionStart == -1)
                {
                    err = new("Missing scope open detected! Scope ends without starting!", line);
                    return true;
                }

                if (curScope.Parent == null)
                {
                    err = new("Program ended unexpectedly!", line);
                    return true;
                }

                AddSection(charIndex);
                sectionStart = charIndex + 1;

                curScope = curScope.Parent;
                continue;
            }
        }

        if (curScope != mainScope)
        {
            err = new("Program missing scope close! Did you forget a '}'?", line);
            return true;
        }

        AddSection(code.Length); // to ensure the end of code (if in global scope) is counted

        return false;
    }

    private static bool CreateVariables(string code)
    {
        return true;
    }
}