package com.ticketflow.identity_service.service;

import com.ticketflow.identity_service.models.User;
import org.springframework.stereotype.Component;

import javax.crypto.spec.SecretKeySpec;
import javax.xml.bind.DatatypeConverter;
import java.security.Key;

import java.util.Date;

import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.Claims;
import io.jsonwebtoken.JwtBuilder;

@Component
public class JwtGenerator {
    private static final int HOURS_IN_DAY = 24;
    private static final String SECRET_KEY = "top_secret";
    private static final String JWT_ISSUER_NAME = "IdetityService.JwtGenerator";

    public String generate(User user) {
        return generate(user, 7 * HOURS_IN_DAY);
    }

    public String generate(User user, int expireHours) {
        return createJWT(createRandomId(user), JWT_ISSUER_NAME, user.getEmail(), convertHoursToMillSeconds(expireHours));
    }

    public Claims decodeJWT(String jwt) {
        //This line will throw an exception if it is not a signed JWS (as expected)
        return Jwts.parser()
                .setSigningKey(DatatypeConverter.parseBase64Binary(SECRET_KEY))
                .parseClaimsJws(jwt).getBody();
    }

    //Sample method to construct a JWT
    private String createJWT(String id, String issuer, String subject, long ttlMillis) {

        //The JWT signature algorithm we will be using to sign the token
        SignatureAlgorithm signatureAlgorithm = SignatureAlgorithm.HS256;

        long nowMillis = System.currentTimeMillis();
        Date now = new Date(nowMillis);

        //We will sign our JWT with our ApiKey secret
        byte[] apiKeySecretBytes = DatatypeConverter.parseBase64Binary(SECRET_KEY);
        Key signingKey = new SecretKeySpec(apiKeySecretBytes, signatureAlgorithm.getJcaName());

        //Let's set the JWT Claims
        JwtBuilder builder = Jwts.builder().setId(id)
                .setIssuedAt(now)
                .setSubject(subject)
                .setIssuer(issuer)
                .signWith(signatureAlgorithm, signingKey);

        //if it has been specified, let's add the expiration
        if (ttlMillis >= 0) {
            long expMillis = nowMillis + ttlMillis;
            Date exp = new Date(expMillis);
            builder.setExpiration(exp);
        }

        //Builds the JWT and serializes it to a compact, URL-safe string
        return builder.compact();
    }

    private long convertHoursToMillSeconds(int hours) {
        int secondsInHour = 60 * 60;
        int millSecondsInSecond = 1000;
        
        return hours * secondsInHour * millSecondsInSecond; 
    }

    private String createRandomId(User user) {
        return String.valueOf(user.hashCode());
    }
}