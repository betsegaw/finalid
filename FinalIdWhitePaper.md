# Decentralized Social Identity

## Introduction

In an increasingly digital world, online interactions and services relay on being able to maintain an identity and its associated properties. While many companies and foundations have attempted to put together centralized solutions for digital identities, none have succeeded in creating a unified solution that is universally appealing.

This paper is aimed at briefly summarizing the shortcomings of popular existing identity solutions as well as highlighting their shared fundamental issues. It will then go on to propose a solution that addresses many of the same issues while providing a robust platform for building services on top.

## Requirements for digital identity

1. **Map a digital identity to a physical world identity**

    Most scenarios related to identity require mapping the real world identity of a person to a digital representation. Creating this mapping is the most difficult part of creating digital identities.

2. **Work across countries and companies**

    There are many challenges with coming up with a unified identity solution that countries that are politically at odds with on another can all work with. One of the most impressive achievements of the internet is how so many countries that are declared enemies of each other still participate in being part of the same network of computers. Replicating that level of success with identity would help pair the internet with a consistent, personal identity, unlocking many scenarios.

3. **Resilient to malicious actions**

    Many malicious actions in the digital space are made possible either through creating completely fake identities, hijacking existing identities or tracking the activities of digital identities. Any centralized and widely adopted digital system will need to architecturally address these risks as well as minimizing the propagation of inevitable compromises.

4. **Anonymity capable**

    While most of the actions we take in the digital space might not require anonymity, certain other activities such as online voting do. While absolute anonymity is common and possible today, preventing duplicate accounts as a basic requirement as well as enabling exposing certain properties while remaining anonymous (such as proving citizenship without exposing which citizen during voting) unlocks key capabilities in the digital space.

## Today's Digital Identity Landscape

Most of the identity mechanisms used today are based on email verification. When creating a login to a website, most sites will require an email address to sign up and additionally, some website require a working phone number to allow you to sign up. The secondary step of phone verification is meant to stop users from being able to create multiple accounts.

Most email provides require phone verification on first sign up but, assuming you are in possession of a phone number, you are good to go. If you run your own email server, you can create any number of email addresses you want and use them to sign up for many services that don't require phone number verifications.

The sign-up mechanism of using your email address as a fundamental identification method has many weaknesses.

1. Having your own domain allows you to bypass the email verification step and create any number of accounts
2. Many services exists across the web that grant you access to a temporary phone address.
