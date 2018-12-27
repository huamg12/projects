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


#define P_Template(T, Type)           \
    Type data;                   \
    int (*Val)(T* that, int p);

#define Plate_CP(T, Type)    P_Template(T, Type)


#define __SUPER(Base, Type, ...)            \
    union {                                 \
        Base super;                         \
        struct {                            \
            Plate_##Base(Type, __VA_ARGS__) \
        };                                  \
    }


typedef struct P CP;
struct P
{
    Plate_CP(CP, int);
    int val;
};

typedef struct S CS;
struct S {
    __SUPER(CP,CS,int);
    int val;
};
