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

typedef C_class First       CFirst;
typedef C_class Second      CSecond;


#define CFirst_Template(T, Type)                    \
    Type data;                                      \
    int (*Val)(T* that, int p);

#define MemberOf_CFirst(T, Type)    CFirst_Template(T, Type)


#define __SUPER(Base, Type, ...)                    \
    union {                                         \
        Base super##Base;                                 \
        struct {                                    \
            MemberOf_##Base(Type, __VA_ARGS__)      \
        };                                          \
    }

C_class First
{
    MemberOf_CFirst(CFirst, int);
    int val;
};

C_class Second
{
    __SUPER(CFirst,CSecond,int);
    int val;
};

#endif //_C_TEMPLATE_H_.
