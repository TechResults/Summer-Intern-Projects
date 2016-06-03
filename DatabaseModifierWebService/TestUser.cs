using System;

public class TestUser
{
    string _firstName;
    string _lastName;
    string _mobileNumber;
    int _PIN;
    long _SSN;
    Guid _UserToken;
    int _acessedNum;
    long _ID;
    int _option;


        //Constructor
    public TestUser()
    {
        _acessedNum = 0;
    }

    public string firstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public string lastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public string mobileNumber
    {
        get { return _mobileNumber; }
        set { _mobileNumber = value; }
    }

    public int PIN
    {
        get { return _PIN; }
        set { _PIN = value; }
    }

    public long SSN
    {
        get { return _SSN; }
        set { _SSN = value; }
    }

    public Guid userToken
    {
        get { return _UserToken; }
        set { _UserToken = value; }
    }

    public int loyaltyVal
    {
        get { return _acessedNum; }
        set { _acessedNum = value; }
    }

    public long ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    public int option
    {
        get { return _option; }
        set { _option = value; }
    }
}

