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

/* abstract understand. use A not CFirst; use 'struct A' not Class Father.
 * it's hard to understand  */

//class P, and S (parent, sun)
int Value( CFirst* here, int p );
int Sun001Calc( CSun001* here, int s );
int Sun002Calc( CSun002* here, int s );

CFirst* _CFirst( CFirst* here, int data )
{
    here->data = data;
    here->Multi = Value;
    return here;
}
int Value( CFirst* here, int p )
{
    printf("father member data is %d\n", here->data * p);
    return here->data * p;
}
// class CS.
CSun001* _CSun001( CSun001* here, int val )
{
    _CFirst( &here->FATHER(CFirst), val );
    here->Multi = Sun001Calc;
    here->val = val;
    return here;
}
int Sun001Calc( CSun001* here, int s )
{
    printf("s01 member data is %d\n", here->data * s);
    return here->data * s;
}

// class CSun002
CSun002* _CSun002( CSun002* here, int val )
{
    _CFirst( &here->FATHER(CFirst), val );
    here->Multi = Sun002Calc;
    here->val = val;
    return here;
}
int Sun002Calc( CSun002* here, int s )
{
    printf("s02 member data is %d\n", here->data * s);
    return here->data * s;
}

//====== main =====//
int main(void)
{
    //===use macro template===//
    CFirst p;
    _CFirst(&p, 10);
    p.val = 4;
    p.Multi(&p, 2);

    CSun001 s01;
    _CSun001(&s01, 30);
    s01.val = 5;
    s01.Multi(&s01, 3);

    CSun002 s02;
    _CSun002(&s02, 25);
    s02.val = 5;
    s02.Multi(&s02, 5);

    //multi state.
    puts("\nTest multiple state:\n");
    CFirst* active = NULL;

    active = &p;
    active->Multi(active, 1);

    active = (CFirst*)(&s01);
    active->Multi(active, 1);

    active = (CFirst*)(&s02);
    active->Multi(active, 1);



//    puts("!!!Hello World!!!"); /* prints !!!Hello World!!! */
	return EXIT_SUCCESS;
}
