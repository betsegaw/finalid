# Identity based on graph connections

## Summary

A primary challenge of the digital age is identity assessment and identity verification. The problem comes in two forms,

1. Defining identity in the digital world
2. Verifying that the person we are interacting with is in possession of said identity

### 1. Defining identity

What does it mean to be "Betsegaw Tadele" (thats my name 😊)? With my handle, in different context it means different things. For my parents I am their son, for my company, I am it's employee, etc...

Two simple axioms that I will put forward are these,

1. Identity ("I am Betsegaw Tadele", which going forward will be mentioned as identity 1) is only important in context of the relationship that the asserted identity has with other identities that people claim.
2. Due to (1), protecting the identity assertion is not important but rather the relationship "Identity 1 is the son of Identity 2 (my father)"

With this in mind, any identity is the sum total of it's relationship with other identity. As an aside, assertions such as "Identity 1 is a great cleaner" can be broken down to "Identity 2 endorses identity 1 as a competent cleaner". Therefore, even assertions that we tend to think of as independent properties of an identity (eg. competency at a task) is only meaningful in context of  other people's endorsement of said competency.

Within the context of the assumptions of  the current digital age, the only thing that anyone on the internet can independently prove (i.e without confirmation from another entity) is that they are in possession of a private key. Therefore, all of the following protocols are designed with the assumption that this always holds true.

## Vocabulary (non-technical)

**Encrypt** - Convert information (eg. "Identity 1 has $0 in his bank account")  into something that is unreadable (eg."Jefoujuz 2 ibt %1 jo ijt cbol bddpvou") to anyone who does not know the secret of how to revert back the identity (eg. To read the hidden information, switch each character with the previous character, and numbers with the previous value, and symbols with the previous symbol on a QWERT keyboard). While the example uses a very simple method, many other more effective methods exists to make encryption more resistent to attacks.

**Decryption** - Reverting the process of encryption by either using the secret method agreed upon during encryption or by finding a weakness of the method of encryption.

**Public/Private Key pair** - A particular method of encrypting information where two keys, one to be kept private and the other to be shared publicly with others, where content encrypted by the public key can only be decrypted by the private key. Anyone who has the public key of the person to whom they want to send the message can encrypt the content and send it to the target, knowing that only the person who has the corresponding private key can decrypt it. An interesting side effect here is that the private key can also be used to encrypt content that can only be decrypted by the public key.

**Signing** - Public/Private Key pairs have the added benefit that the encryption also works the other way (i.e. content encrypted by a private key can only be decrypted by the public key). This allows a person to take content that they want to sign (eg. "I, Betsegaw Tadele, have $0 in my bank account") and encrypt it and share it such that people can take the encrypted content and decrypt it using the public key and with certainty that the person who encrypted it has access to the matching private key and thus did indeed agree with the statement above.

## Onboarding Story

Having moved recently, John decided to try and simplify his life this time around and start using the **id-graph** he had heard so much about. He takes his existing phone and downloads the **id-app**. After finishing the install, the app asks him if he already has an existing id on id-graph. He doesn't so it asks him to fill out a few basic bits of information about himself. Once he is done, the app lets him know that he should install this app on another device as well so that he can save himself time if he loses this device or something happens to it. For now, he doesn't do that and the setup is complete. 

To get started with getting endorsements that differentiate his online identity from other spam accounts that get created, he phones his brother and a couple of his friends to endorse his account as real. Some of them simply endorse the fact that it is a real account and some endorse their own specific relationship with him on there as well, such as Lily, who is his roommate. He accepts all of those endorsements, meaning the app will track and store those endorsements.

For the next step, he goes to the DMV to switch his existing physical license to an id-graph license. They verify his physical id and then, using a copy of the app that they have at the DMV, they verify his online identity as being in possession of a license for a specific period of time. His app, knowing the expiration date of the endorsement, will take care of requesting an endorsement update on behalf of John and the DMV will grant the license if John still meets all the requirements and will work with the app on John's phone to get all the necessary information.

For the next step, John goes to USCIS (the United States Citizenship and Immigration Services) and gets an endorsement of his id proving that he is a US citizen. Once he has acquired said approval, he can now use that endorsement to create an anonymous account at a service that provides a social media platform where users are grouped by their country of citizenship to talk about and share news about their own country with each other. John feels reasonably safe to express his views without worrying that a hack of the social media platform would expose his identity.

## Protocol specifics (high-level)

### Identity

A singe identity in the id-graph world is a unique GUID. This identity itself is a handle for information and is relatively meaningless. It is the various assetions and their related endorsements that give it meaning and value.

Examples,

- Identity 1
- Identity 2
- Identity 3

### Proving Key ownership (eg. during Identity show scenario)

In order to show that the device owns the particular key the flow is as follows,

1. The user trying to verify displays a QR code with a random string on it
2. The user trying to prove identity scans that QR code, signs it with their private key and displays it
3. The user trying to verify scans the QR code with the signed string and verifies the signature source

### Assertion

Assertions are unverified statements about an identity on the graph.

TODO: Are assertions about an identity being mapped to a real entity (as opposed to spam or fake identities) useful?
TODO: Assertions and their public vs. private (encrypted) status.

Examples are as follows,

- "Identity 1 has a drivers license that is set to expire in 2/2/2021"
- "Identity 1 is the son of Identity 2"
- "Identity 1 is a real person" (this one is a unique case since this would be how we would verify whether a person is real or not)

### Identity Keys

This is a public key that asserts that the private key to a particular public key is help by the person to whom the identity belongs. This are stored per device that the user owns. Any number of assertions of identity keys can be made. The difference will always be in the endorsements that the assertions get and the score of the endorsements.

Examples,

- Identity 1 is owned by Public key 1
- Identity 1 is owned by Public key 2
- Identity 1 is owned by Public key 3
- Identity 2 is owned by Public key 4
- Identity 3 is owned by Public key 5

**Note**: There are can multiple key assertions. This might be due to multiple devices (in which case each key will endorse the other keys) or because spammers are trying to take over the identity list.

### Endorsements

Once an assertion has been made about an identity, the assertion is then verified and agreed upon by other entities, who would endorse the point that the user does indeed have said property. They do so using one of their asserted identity keys. It is this interplay between assertions and verification by identity keys that forms the basis of the validity of everyone's assertions.

Examples,

- Public key 1 endorses Public key 2 as also being mapped to Identity 1 - 2/24/2020 10:41:00 PM
- Public key 2 endorses Public key 1 as also being mapped to Identity 1 - 2/24/2020 10:41:00 PM
- Public key 2 endorses "Identity 1 is the son of Identity 2" - 2/24/2020 10:41:00 PM

The final appended date and time are the times on which the endorsements occurred.

There are two types of endorsements. Public and private.

The most common type of public endorsements are key endorsement (i.e. endorsements about a public key being mapped to an identity)

Most other endorsement types are private endorsements. This have a very similar content as the public endorsements except that they are additionally encrypted and stored with the endorsement receivers private key. This way, the endorsement receiver can share their endorsement selectively (eg. Their social security numbers).

### Expiration of Endorsement and automatic renewal

The app on the users devices have access to the endorsements that the user has received and thus, for time limited endorsements, can automatically request renewal of endorsements on behalf of the user.

### Revoking Endorsements

Endorsements can be revoked by posting a reverse endorsement that invalidates the previous endorsement.

Example,

- Public key 2 does not endorse Public key 1 as also being mapped to Identity 1 - 2/25/2020 10:00:00 PM

Also, this revocation can be triggered by the app itself when it sees activity on an identity that indicate loss of control of one or more private keys that had been previously accepted.

### Identity Scoring

TODO: Explain (come up with) a scoring mechanism for how each entities endorsements can be scored as being reliable or not relative to another entity.

### Anonymous Endorsement Usage

TODO: Explain the mechanism with which a service can use endorsements without storing the mapping of the internal representation of the user with the identity of the endorsement owner.

One key scenario this whole system tries to address is the usage of endorsements with minimal exposure of the relation of an endorsement with the identity.

Example scenarios,

- **Voting** - People need to prove they are eligible to vote while minimizing the association between their identity and the choice they made about the candidate they voted for
- **Free speech** - People need to prove they are not a bot while still maintaining their ability to be anonymous, which they can exercise if they choose to do so.

The way this will be accomplished is through the creation of intermediary handles. The process looks as follows

1. The user will connect with the service and share with them the proof that they are indeed in possession of the endorsement they wish to use (eg. Endorsed by the USCIS as being a citizen)
2. The service will then take that information and
  a. create an intermediate handle (eg. a user name)
  b. store the Identifier of the proof of endorsement and the intermediate handle separately, with no retention of the association after step c
  c. create a statement "Identity X owns intermediate handle y", sign it and encrypt it using the users identity and return it back.
  d. user can now store this

This way, it prevents people from creating multiple accounts, provides a reasonable degree of anonymity as well as ensuring that the user can recover access in the event that they lose access to their account and need to get their user name back.

### Compromises

The entire premise behind Final ID is to mirror real world identity into the digital space. By this very premise, we know that it will have the same weaknesses as real world identity.

- Bad actors can and will forge false identities by convincing unknowing victims to sign/endorse their keys
  - *Mitigation*: unlike in the real world, the story unfolds in public and thus is auditable.
- People with few social connection will have difficulty onboarding because of lack of endorsements.
  - *Mitigation*: When social institutions get involved, they will help alleviate this issue by endorsing people whose social connection might be low.

Beyond just the mirroring issues, we also had to make a couple of compromises,

- The anonymity portion requires a TOFU (Trust on first use) with the service they are authenticating with. If the institution has already been compromised, then the anonymity will fall through.
  - *Mitigation*: None yet. 

Finally, the contributors to the code so far are not security experts and most likely are making laughable mistakes. However, our goal is to put together a good enough start so that other people who have more experience in this area can help out in improving and fixing the implementation. 
