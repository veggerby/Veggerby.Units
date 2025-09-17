# Code Formatting & Style Guide (Authoritative Details)

This document augments `.editorconfig` and the AI contribution guidelines with explicit, example‑driven rules for bracket / parenthesis usage and whitespace (horizontal & vertical). These rules are binding.

## 1. Indentation & Whitespace (Horizontal)

- ALWAYS use spaces for indentation. Tabs are forbidden. (`indent_style = space` already enforced).
- Indent size: 4 spaces for C# (`indent_size = 4`).
- No trailing whitespace at line ends.
- One space after commas in parameter / argument / generic type lists.
- No space before commas.
- Binary operators surrounded by a single space on both sides (e.g. `a + b`, `x * y`).
  - Exception: Unary operators have no separating space (`-value`, `!flag`).
- No space after a cast: `(int)value` (per `csharp_space_after_cast = false`).
- No space between method name and its opening parenthesis: `DoWork(arg1, arg2);`.
- No space inside parentheses for method calls, declarations or control flow: `if (condition)` not `if ( condition )`.
- Indexers: no space before `[` and no space inside brackets: `items[0]`.
- Generics: no spaces around `<T>`.
- Avoid aligning with extra spaces: prefer single space indentation semantics only.

## 2. Line Breaks & Vertical Spacing

- Newline sequence: CRLF (repository setting). Do not mix endings within a file.
- File ends WITHOUT an extra blank line if not already present ( `insert_final_newline = false`).
- Keep a single blank line between logically distinct members (fields, constructors, properties, methods, operators, nested types).
- Inside test methods enforce visual Arrange / Act / Assert separation via exactly one blank line before each zone comment (`// Arrange`, `// Act`, `// Assert`).
- Avoid multiple consecutive blank lines (max one) except when clearly isolating large conceptual sections (rare; justify in PR).
- Chained fluent calls may break after the `.` and align subsequent lines one indentation level in (avoid vertical alignment with artificial spaces).

Example:

```csharp
var unit = Prefix.k * Unit.SI.m
    / (Unit.SI.s ^ 2)
    * Unit.SI.kg;
```

## 3. Braces & Blocks

- ALWAYS use braces `{}` for all control statements (if/else/for/foreach/while/do/using/switch/case blocks) even if the body is a single statement.
- Opening brace is on the same line as the construct for types, methods, properties, lambdas requiring block bodies, control statements. (`csharp_new_line_before_open_brace = all`).

Example:

```csharp
if (value < 0)
{
    throw new ArgumentOutOfRangeException(nameof(value));
}
```

- Expression-bodied members are allowed only where they materially improve clarity (e.g., trivial property getters, `ToString`, small forwarding methods). Avoid for anything containing conditionals or multiple operations.
- Single-line block bodies may be preserved (formatter setting) but prefer multi-line style when adding new non-trivial logic.

## 4. Parentheses

- Use parentheses to enforce clarity in mixed arithmetic or logical expressions, even when operator precedence would make them optional (rule: prefer clarity over terseness). This is consistent with `always_for_clarity` settings for arithmetic, relational and other binary operators.
- Avoid redundant double-parentheses: prefer `(a * b) + c`, not `((a * b)) + c`.
- Pattern matching & casts: minimal parentheses required by syntax only.

## 5. Using Directives

- System namespaces first, then a blank line, then other namespaces (enforced by analyzers).
- Grouped and sorted; no unused usings. Keep outside the namespace (`csharp_using_directive_placement = outside_namespace`).

## 6. Blank Lines Within Members

Insert blank lines to separate:

- Logical phases inside complex methods (validation, transformation, return) sparingly.
- In tests, strictly: Arrange / Act / Assert groups.

Do NOT insert blank lines:

- At the start or end of a block.
- Between a documentation comment and its target symbol.

## 7. Documentation Comments

- XML doc `<summary>` immediately precedes the symbol, no blank line between them.
- Blank line between multi-line documentation comment and the next code member if they are conceptually distinct.

## 8. Naming Recap (See .editorconfig for analyzer-enforced rules)

- Private fields: `_camelCase`.
- Constants: `PascalCase` (even when private) per repo rule.
- No Hungarian notation or type suffixes.

## 9. Example (Composite Operator Implementation)

```csharp
public static Unit operator *(Unit left, Unit right)
{
    if (left == Unit.None)
    {
        return right;
    }

    if (right == Unit.None)
    {
        return left;
    }

    if (IsAffine(left) || IsAffine(right))
    {
        throw new UnitException(left, right);
    }

    return OperationUtility.RearrangeMultiplication(x => x.Multiply((a, b) => a * b, Unit.None), (x, y) => x / y, left, right)
        ?? OperationUtility.ReduceMultiplication(x => x.Multiply((a, b) => a * b, Unit.None), (x, y) => x ^ y, left, right)
        ?? Unit.Multiply(left, right);
}
```

## 10. Test Method Template

```csharp
[Fact]
public void GivenCondition_WhenAction_ThenOutcome()
{
    // Arrange
    var m1 = new Int32Measurement(2, Unit.SI.m);
    var m2 = new Int32Measurement(3, Unit.SI.m);

    // Act
    var sum = m1 + m2;

    // Assert
    sum.Value.Should().Be(5);
}
```

## 11. Enforcement

- Run `dotnet format` locally before committing (already successful in CI).
- PRs introducing tabs, inconsistent spacing, or brace omissions will be rejected.
- Automated analyzers enforce a subset; the remainder (e.g., vertical grouping discipline) is enforced during review.

## 12. Rationale Highlights

- Consistent horizontal spacing improves diff readability and minimizes merge conflicts.
- Mandatory braces eliminate an entire class of subtle control-flow bugs.
- Parentheses for clarity reduce cognitive load in dense algebraic unit expressions.

If any rule conflicts with `.editorconfig`, `.editorconfig` wins; update this document instead of deviating in code.
