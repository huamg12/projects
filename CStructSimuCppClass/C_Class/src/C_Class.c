/*
 ============================================================================
 Name        : C_Class.c
 Author      :
 Version     :
 Copyright   : Your copyright notice
 Description : Hello World in C, Ansi-style

 Refer       : https://blog.csdn.net/stophin/article/details/54646796.

 ============================================================================
 */

#include <stdio.h>
#include <stdlib.h>

#include "../inc/C_template.h"

//! 01-use function pointer. int (*pfunc)(int, int) !!!;
//! 02-constructor function, and this pointer.
//! 03-inherit. no-name struct in union !!!.
//! 04-overwrite.
//! 05-Template, macro (NO need to write again !!!).
//! 06-multi-level inherit. (macro can not nesting.)

//#define __CALL(o, f, ...) o.f(&o, __VA_ARGS__)
//__CALL(a, Val, 10);

//class P, and S (parent, sun)
int siFatherCalc( CFather* here, int p );
int siSun001Calc( CSun001* here, int s );
int siSun002Calc( CSun002* here, int s );

CFather* _CFather( CFather* here, int data )
{
    here->data = data;
    here->Calc = siFatherCalc;
    return here;
}
int siFatherCalc( CFather* here, int p )
{
    int siTemp = here->data + p;    //father to "Add" calculate.
    printf("====member data: %d, input data: %d\n", here->data, p);
    printf("father + calculated result is %d\n", siTemp);
    return siTemp;
}
// class CS.
CSun001* _CSun001( CSun001* here, int val )
{
    _CFather( &here->FATHER(CFather), val );
    here->Calc = siSun001Calc;
    here->val = val;
    return here;
}
int siSun001Calc( CSun001* here, int s )
{
    int siTemp = here->data - s;    //sun001 to "Sub" calculate.
    printf("====member data: %d, input data: %d\n", here->data, s);
    printf("s01 - calculated result is %d\n", siTemp);
    return siTemp;
}

// class CSun002
CSun002* _CSun002( CSun002* here, int val )
{
    _CFather( &here->FATHER(CFather), val );
    here->Calc = siSun002Calc;
    here->val = val;
    return here;
}
int siSun002Calc( CSun002* here, int s )
{
    int siTemp = here->data * s;    //sun002 to "Multiple" calculate.
    printf("====member data: %d, input data: %d\n", here->data, s);
    printf("s02 x calculated result is %d\n", siTemp);
    return siTemp;
}

//====== main =====//
int main(void)
{
    //===use macro template===//
    CFather p;
    _CFather(&p, 10);
    p.val = 4;
    p.Calc(&p, 2);

    CSun001 s01;
    _CSun001(&s01, 30);
    s01.val = 5;
    s01.Calc(&s01, 3);

    CSun002 s02;
    _CSun002(&s02, 25);
    s02.val = 5;
    s02.Calc(&s02, 5);

    //polymorphism
    puts("\nTest polymorphism:\n");
    CFather* active = NULL;

    active = &p;
    active->Calc(active, 1);

    active = (CFather*)(&s01);
    active->Calc(active, 1);

    active = (CFather*)(&s02);
    active->Calc(active, 1);


//    puts("!!!Hello World!!!"); /* prints !!!Hello World!!! */
	return EXIT_SUCCESS;
}
