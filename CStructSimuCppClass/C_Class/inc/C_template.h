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

typedef struct P CP;

#define P_Template              \
    int data;                   \
    int (*Val)(CP* that, int p);

#define Plate_CP    P_Template

typedef struct P
{
    Plate_CP
    int val;
} P;

#define __SUPER(Base)           \
    union {                     \
        Base super;             \
        struct {                \
            Plate_##Base        \
        };                      \
    }

typedef struct S {
    __SUPER(CP);
    int val;
}CS;
