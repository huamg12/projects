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

/* abstract understand. use A not CFather; use 'struct A' not Class Father.
 * it's hard to understand  */
//class A.
//typedef struct A A;
//struct A
//{
//    int data;
//    int (*Val)( A* that, int a );
//};
//int Val( A* that, int a )
//{
//    printf("data a is %d\n", that->data + a);
//    return that->data + a;
//}
//A * _A( A * that, int data )
//{
//    that->data = data;
//    that->Val = Val;
//    return that;
//}
//
////class B
//typedef struct B B;
//struct B
//{
//    union
//    {
//        A super;
//        struct
//        {
//            int data;
////            int (*Val)( A* that, int a );
//            int (*Val)( B* that, int a ); //implicit convert to B *. !!!
//        };
//    };
//    int val;
//};
//B* _B( B* that, int val )
//{
//    _A(&that->super, val);
//    that->val = val;
//    return that;
//}


//class P, and S (parent, sun)
CFirst* _P( CFirst* here, int data )
{
    here->data = data;
    here->Val = Value;
    return here;
}
int Value( CFirst* here, int p )
{
    printf("p member data is %d\n", here->data * p);
    return here->data * p;
}
// class CS.
CSecond* _S( CSecond* here, int val )
{
    _P( &here->FATHER(CFirst), val );
    here->Val = myVal;
    here->val = val;
    return here;
}
int myVal( CSecond* here, int s )
{
    printf("s member data is %d\n", here->data * s);
    return here->data * s;
}

//====== main =====//
int main(void) {


//    A a;
//    _A( &a, 20 );
//    a.Val( &a, 10 );
//
//
//    B b;
//    _B( &b, 30 );
//    b.Val( &a, 3); //not &b ???

    //===use macro template===//
    CFirst p;
    _P(&p, 40);
    p.val = 4;
    p.Val(&p, 10);

    CSecond s;
    _S(&s, 50);
    s.val = 5;
    s.Val(&s, 20);

	puts("!!!Hello World!!!"); /* prints !!!Hello World!!! */

	return EXIT_SUCCESS;
}