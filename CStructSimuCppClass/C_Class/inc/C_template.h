/*
 ============================================================================
 Name        : C_temolate.h
 Author      :
 Version     :
 Copyright   : Your copyright notice
 Description : Hello World in C, Ansi-style

 Refer       : https://blog.csdn.net/stophin/article/details/54646796.
 ============================================================================
 */

#ifndef _C_TEMPLATE_H_
#define _C_TEMPLATE_H_

#include "C_class.h"

// template. NOT USED.
/***
#define Template(T1, T2, Type1, Type2)              \
    Type1 data1;                                    \
    Type2 data2;                                    \
    int (*func_1)(T1* p1, int p2);                  \
    int (*func_2)(T2* p1, int p2);
***/

//01// FATHER class macro.
#define FATHER(BaseClass)           father##BaseClass

//02// single INHERIT macro.
#define __INHERIT(BaseClass, SelfClass)                     \
    union {                                                 \
        BaseClass father##BaseClass;                        \
        struct {                                            \
            MemberOf_##BaseClass(SelfClass)                 \
        };                                                  \
    }

//03// Fahter class Member macro.
#define MemberOf_CFirst(SelfClass)                         \
    int data;                                               \
    int (*Multi)(SelfClass* self, int p);

//04// declare class.
typedef C_class First       CFirst;
typedef C_class Sun001      CSun001;
typedef C_class Sun002      CSun002;

C_class First
{
    MemberOf_CFirst(CFirst);
    int val;
};

C_class Sun001
{
    __INHERIT(CFirst, CSun001);
    int val;
};

C_class Sun002
{
    __INHERIT(CFirst, CSun002);
    int val;
};

#endif //_C_TEMPLATE_H_.
