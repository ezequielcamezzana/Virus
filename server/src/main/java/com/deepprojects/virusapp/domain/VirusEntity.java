package com.deepprojects.virusapp.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity
@Data
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "Virus")
public class VirusEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    @Column(name = "id")
    private Long id;
    @Column(name = "name")
    private String name;
    @Column(name = "seed")
    private String seed;
    @Column(name = "damage")
    private Float damage;
    @Column(name = "projectile_speed")
    private Float projectileSpeed;
    @Column(name = "speed")
    private Float speed;
    @Column(name = "life_points")
    private Float lifePoints;
    @Column(name = "image ")
    private String image;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "owner", referencedColumnName = "id")
    private AccountEntity owner;




    /*
    * 	`id` BIGINT auto_increment NOT NULL,
	`owner` BIGINT NOT NULL,
	`name` varchar(100) NOT NULL,
	`damage` FLOAT NOT NULL,
	`projectile_speed` FLOAT NOT NULL,
	`speed` FLOAT NOT NULL,
	`life_points` FLOAT NOT NULL,
    * */
}
