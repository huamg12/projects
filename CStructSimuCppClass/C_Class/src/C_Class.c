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

//#define __CALL(o, f, ...) o.f(&o, __VA_ARGS__)
//__CALL(a, Val, 10);

int main(void) {

    //class A.
    typedef struct A A;
    struct A
    {
        int data;
        int (*Val)( A* that, int a );
    };
    int Val( A* that, int a )
    {
        printf("data a is %d\n", that->data + a);
        return that->data + a;
    }
    A * _A( A * that, int data )
    {
        that->data = data;
        that->Val = Val;
        return that;
    }
    A a;
    _A( &a, 20 );
    a.Val( &a, 10 );

    //class B
    typedef struct B B;
    struct B
    {
        union
        {
            A super;
            struct
            {
                int data;
                int (*Val)( A* that, int a );
            };
        };
        int val;
    };
    B* _B( B* that, int val )
    {
        _A(&that->super, val);
        that->val = val;
        return that;
    }
    B b;
    _B( &b, 30 );
    b.Val( &a, 3); //not &b ???


	puts("!!!Hello World!!!"); /* prints !!!Hello World!!! */

	return EXIT_SUCCESS;
}
