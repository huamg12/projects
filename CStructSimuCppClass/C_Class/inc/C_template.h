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
#define DT_TYPE                     int                     //Default Template type.

//02// single INHERIT macro.
#define _L1_INHERIT(BaseClass, SelfClass, ...)              \
    union                                                   \
    {                                                       \
        BaseClass father##BaseClass;                        \
        struct                                              \
        {                                                   \
            MemberOf_##BaseClass(SelfClass, __VA_ARGS__)    \
        };                                                  \
    }

//03// Father class Member macro.
//! The name after "MemberOf_" must be the name of basic class.
//! Cause the macro will be expended in "_L1_INHERIT".
#define MemberOf_CFather(SelfClass, TypeName)               \
    TypeName data;                                          \
    int (*Calc)(SelfClass* self, int p);

//! multi-level inherit, define pre-level class member.
#define MemberOf_CSun001(SelfClass, TypeName)               \
        MemberOf_CFather(SelfClass, TypeName)               \
        int val;

// double inherit.
#define MemberOf_CMother(SelfClass, TypeName)               \
    TypeName data;                                          \
    int (*Calc)(SelfClass* self, int p);

#define _DD_INHERIT(BaseClass1, BaseClass2, SelfClass, ...) \
    union                                                   \
    {                                                       \
        BaseClass1 father##BaseClass1;                      \
        struct                                              \
        {                                                   \
            MemberOf_##BaseClass1(SelfClass, __VA_ARGS__)   \
        }b1;                                                \
    };                                                      \
    union                                                   \
    {                                                       \
        BaseClass2 mother##BaseClass2;                      \
        struct                                              \
        {                                                   \
            MemberOf_##BaseClass2(SelfClass, __VA_ARGS__)   \
        }b2;                                                \
    };

//04// declare class.
typedef C_class Father      CFather;
typedef C_class Sun001      CSun001;
typedef C_class Sun002      CSun002;
typedef C_class Baby01      CBaby01;

//for double inherit.
typedef C_class Mother      CMother;
typedef C_class Dear01      CDear01;

C_class Father
{
    MemberOf_CFather(CFather, DT_TYPE);
    int val;
};

C_class Sun001
{
    _L1_INHERIT(CFather, CSun001, DT_TYPE);
    int val;
};

C_class Sun002
{
    _L1_INHERIT(CFather, CSun002, DT_TYPE);
    int val;
};

//for two level inherit.
C_class Baby01
{
    _L1_INHERIT(CSun001, CBaby01, DT_TYPE);
    int cry;
};

// for double inherit
C_class Mother
{
    MemberOf_CMother(CMother, DT_TYPE);
    int val;
};
C_class Dear01
{
    _DD_INHERIT(CFather, CMother, CDear01, DT_TYPE);
    int val;
};

#endif //_C_TEMPLATE_H_.
