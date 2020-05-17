# Mini-Pascal Compiler

Mini-Pascal compiler is a small compiler written at Code Generation course at Helsinki University. It takes Mini-Pascal program as input and generates low-level C-program as output.

## Example input
```
program GCD;
begin
    var i, j : integer;
    read (i, j); {*
	while i <> j+1 do nothing... 
	this is inside a comment
	*}
    while i <> j do
        if  i > j then i := i - j; 
        else j := j - i;      
    writeln (i);  
end.
```

## Example output
```
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

typedef int bool;
#define true 1
#define false 0

// hard coded function to allocate string array
char ** allocateArrayOfStrings(int * arr_size)
{
int tmp_a = sizeof(char);
int tmp_b = *arr_size * tmp_a;
char ** x = malloc(tmp_b);
int var_i = 0;
while_entry: ;
if (var_i >= *arr_size) goto while_exit;
{
int tmp_c = sizeof(char);
int tmp_d = 256 * tmp_c;
x[var_i] = malloc(tmp_d);
var_i = var_i + 1;
}
goto while_entry;
while_exit: ;
return x;
}

// hard coded function to deallocate string array
void deallocateArrayOfStrings(char ** arr, int * arr_size)
{
int var_i = 0;
while_entry: ;
if (var_i >= *arr_size) goto while_exit;
{
free(arr[var_i]);
var_i = var_i + 1;
}
goto while_entry;
while_exit: ;
free(arr);
}

// here are forward declarations for functions and procedures (if any exists)

// here are the definitions of functions and procedures (if any exists)

// here is the main function
int main()
{
int var_i;
int var_j;
scanf("%d %d", &var_i, &var_j);
label_while_0_entry: ;
bool tmp_0 = var_i != var_j;
if (!tmp_0) goto label_while_0_exit;
label_if_0_entry: ;
bool tmp_1 = var_i > var_j;
if (!tmp_1) goto label_else_0_entry;
int tmp_2 = var_i - var_j;
var_i = tmp_2;
goto label_if_0_exit;
label_else_0_entry: ;
int tmp_3 = var_j - var_i;
var_j = tmp_3;
label_if_0_exit: ;
goto label_while_0_entry;
label_while_0_exit: ;
printf("%d\n", var_i);
return 0;
}
```
