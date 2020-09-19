'use strict';
var amqp = require('amqplib/callback_api');
const nodemailer = require('nodemailer');

var fromAddress = '';
var fromAddressPassword = '';
var smtpServer = '';


let transport = nodemailer.createTransport({
    host: smtpServer,
    port: 587,
    auth: {
        user: fromAddress,
        pass: fromAddressPassword
    }
});

amqp.connect('amqp://127.0.0.1', function (connectionError, connection) {
    if (connectionError) {
        throw connectionError;
    }
    connection.createChannel(function (channelError, channel) {
        if (channelError) {
            throw channelError;
        }

        channel.assertQueue('CreditCardInformationSms', {
            durable: false
        });

        channel.consume('CreditCardInformationSms', function (data) {

            var expenseInformation = JSON.parse(data.content.toString());
            var message = expenseInformation.date + ' tarihinde kredi kartýnýzdan ' + expenseInformation.price+' TL tutarýnda alýþveriþ yapýlmýþtýr';

            console.log("Incoming Message : ", data.content);

            sendInformationMail(expenseInformation.email, message);

        }, {
            noAck: true
        });
    });
});


var sendInformationMail = (toAddresses,body) => {

    const message = {
        from: fromAddress,
        to: toAddresses,
        subject: 'Bilgilendirme',
        text: body 
    };
    transport.sendMail(message, function (err, info) {
        if (err) {
            console.log(err)
        } else {
            console.log(info);
        }
    });
}


