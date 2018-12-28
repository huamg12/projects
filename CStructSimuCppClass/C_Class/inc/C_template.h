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
#define FATHER(BaseClass)           father##BaseClass       //For more than one level inherit.

//02// single INHERIT macro.
#define __INHERIT(BaseClass, SelfClass)                     \
    union {                                                 \
        BaseClass father##BaseClass;                        \
        struct {                                            \
            MemberOf_##BaseClass(SelfClass)                 \
        };                                                  \
    }

//03// Father class Member macro.
//! The name after "MemberOf_" must be the name of basic class.
//! Cause the macro will be expended in "__INHERIT".
#define MemberOf_CFather(SelfClass)                         \
    int data;                                               \
    int (*Calc)(SelfClass* self, int p);

//04// declare class.
typedef C_class Father      CFather;
typedef C_class Sun001      CSun001;
typedef C_class Sun002      CSun002;

C_class Father
{
    MemberOf_CFather(CFather);
    int val;
};

C_class Sun001
{
    __INHERIT(CFather, CSun001);
    int val;
};

C_class Sun002
{
    __INHERIT(CFather, CSun002);
    int val;
};

#endif //_C_TEMPLATE_H_.
